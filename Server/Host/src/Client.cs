using Host.Exceptions;
using static Utils.Logger;

using System.Text.Json;
using System.Text.Json.Serialization;

using Host.Json;
using Host.Login;
using Host.Event;

namespace Host;

/// <summary>
///
/// </summary>
internal enum ClientType
{
    /// <summary> </summary>
    Academic,
    /// <summary> </summary>
    Common,
    /// <summary> </summary>
    Invalid
}

/// <summary>
///
/// </summary>
internal enum Bmi
{
    /// <summary> </summary>
    Underweight3,
    /// <summary> </summary>
    Underweight2,
    /// <summary> </summary>
    Underweight1,
    /// <summary> </summary>
    Normal,
    /// <summary> </summary>
    Overweight,
    /// <summary> </summary>
    Obese1,
    /// <summary> </summary>
    Obese2,
    /// <summary> </summary>
    Obese3,
}

/// <summary>
///
/// </summary>
internal sealed class Client : Person, ILogin
{
    #region attributes

    /// <summary>
    ///     The type of the client assigned by the system.
    /// </summary>
    public ClientType ClientType { get; private set; }

    /// <summary>
    /// </summary>
    public Subscription? subscription { get; private set; }

    /// <summary>
    ///     The client's height in meters.
    /// </summary>
    public double Height { get; private set; }

    /// <summary>
    ///     The client's weight in kilograms.
    /// </summary>
    public double Weight { get; private set; }

    /// <summary>
    ///     The client's BMI value.
    /// </summary>
    internal double BmiValue { get => Weight / (Height * Height); }

    /// <summary>
    ///     Get current Bmi situation, for the average person.
    /// </summary>
    internal Bmi CurrentBmi
    {
        get => BmiValue switch
        {
            <
                3.5 =>
                throw new InvalidClientDataException("BMI not valid, too small"),
            <
                16 => Bmi.Underweight3,
            <
                17 => Bmi.Underweight2,
            <
                18.5 => Bmi.Underweight1,
            <
                25 => Bmi.Normal,
            <
                30 => Bmi.Overweight,
            <
                35 => Bmi.Obese1,
            <
                40 => Bmi.Obese2,
            <
                100 => Bmi.Obese3,
            _ => throw new InvalidClientDataException("BMI not valid, too high")
        };
    }

    /// <summary>
    ///     The client's invoices.
    /// </summary>
    private List<Invoice?>? invoices;

    /// <summary>
    ///     The client's credit cards.
    /// </summary>
    private List<CreditCard?>? cc;

    /// <summary>
    ///
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="gender"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="nif"></param>
    /// <param name="address"></param>
    public Client(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, ulong nif, Address address, string email)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email) { }

    #endregion

    /// <summary>
    /// Register a new client.
    /// Client's class constructor.
    /// </summary>

    #region methods

    /// <summary>
    ///
    /// </summary>
    /// <param name="height"></param>
    internal void UpdateHeight(double height)
    {
        try
        {
            Height =
                height is < 2.4 and > 1.0
                    ? height
                    : throw new InvalidClientDataException("Height is not Valid");
        }
        catch (InvalidClientDataException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="weight"></param>
    internal void UpdateWeight(double weight)
    {
        try
        {
            Weight =
                weight is < 200 and > 20
                    ? weight
                    : throw new InvalidClientDataException("Weight is not Valid");
        }
        catch (InvalidClientDataException e)
        {
            Log.Error(e);
        }
    }

    public static async Task test()
    {

        try
        {
            var addr = Address.GenExample1();

            var client1 = new Client("Diogo", "Antunes", Gender.Male,
                                     new DateTime(2002, 05, 20), 100000002, addr,
                                     "a21144@alunos@ipca.pt");

            client1.Weight = 60.2;
            client1.Height = 1.71;
            client1.ClientType = ClientType.Common;

            client1.subscription = new Subscription();

            client1.subscription.Type = SubscriptionPlan.Premium;

            var cc = client1.cc != null ? client1.cc[0] : null;

            var invoice = await Invoice.GetAsync(PaymentType.CreditCard, 8.0, cc,
                                                 DateOnly.FromDateTime(DateTime.Now));

            if (invoice != null)
                await Invoice.GenerateInvoicePdf(client1);
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    internal Session GymEntrance() =>
        subscription != null && subscription.Status != SubscriptionStatus.Inactive
            ? Session.RegisteredEntry
            : Session.UnAuthorized;

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    internal static List<double>? GetImcHistory() { return default; }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogIn() { throw new NotImplementedException(); }

    private protected override
        Task InsertUser(Person user) => throw new NotImplementedException();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogOut() => throw new NotImplementedException();

    // public static async Task Example1()
    //{
    //     var client1 = new Client();

    //    client1.ClientType = ClientType.Common;

    //    var json =
    //        JsonSerializer.Serialize(client1, ClientJsonContext.Default.Client);

    //    Console.WriteLine(json);

    //    client1.subscription = new Subscription();
    //}

    #endregion
}
