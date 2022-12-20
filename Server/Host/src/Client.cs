using Host.Exceptions;
using static Utils.Logger;

using System.Text.Json;
using System.Text.Json.Serialization;

using Host.Json;

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
internal partial class Client : Person, ILogin
{
    #region attributes

    /// <summary>
    /// The type of the client assigned by the system.
    /// </summary>
    public ClientType ClientType { get; private set; }

    /// <summary>
    /// </summary>
    public Subscription? subscription { get; private set; }

    /// <summary>
    /// </summary>
    private CreditCard? cc;

    /// <summary>
    /// The client's height in meters.
    /// </summary>
    public double Height { get; private set; }

    /// <summary>
    /// The client's weight in kilograms.
    /// </summary>
    public double Weight { get; private set; }

    /// <summary>
    /// The client's BMI value.
    /// </summary>
    internal double BmiValue { get => Weight / (Height * Height); }

    /// <summary>
    /// Get current Bmi situation, for the average person.
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

    #endregion

    /// <summary>
    /// Register a new client.
    /// Client's class constructor.
    /// </summary>
    public Client() { }

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
    LoginStatus ILogin.Login() { throw new NotImplementedException(); }

    private protected override Task InsertUser(Person user)
    {
        throw new NotImplementedException();
    }

    public static async Task Example1()
    {
        var client1 = new Client();

        client1.ClientType = ClientType.Common;

        var json =
            JsonSerializer.Serialize(client1, ClientJsonContext.Default.Client);

        Console.WriteLine(json);

        client1.subscription = new Subscription();

        client1.cc = new CreditCard(33, DateTime.Now, DateTime.Now, "34", "name",
                                    CcType.Visa);

        var invoice = await client1.subscription.GenerateInvoiceForCurrentMonth(
            Payment.PaymentType.MbRef, null);

        if (invoice != null)
        {
            Console.WriteLine(invoice.Month);
            Console.WriteLine(invoice.Status);
            Console.WriteLine(invoice.PaidDate);
            Console.WriteLine(invoice.PaymentTypeUsed);
            Console.WriteLine(invoice.ExpiryDate);
            if (invoice.MbReference != null)
            {
                Console.WriteLine(invoice.MbReference.Value);
                Console.WriteLine(invoice.MbReference.ExpiryDate);
            }
        }
    }

    #endregion
}
