using Host.Exceptions;
using static Utils.Logger;
using static Data.DataBase;

using System.Text.Json;
using System.Text.Json.Serialization;

using Host.Json;
using Host.Login;
using Host.Event;
using Data;

namespace Host;

/// <summary>
///     Client Type
/// </summary>
internal enum ClientType
{
    /// <summary> Academic type </summary>
    Academic,
    /// <summary> Common Type </summary>
    Common,
    /// <summary> Invalid </summary>
    Invalid
}

/// <summary>
///     Bmi enum.
/// </summary>
internal enum Bmi
{
    /// <summary> Bmi Underweight 3 </summary>
    Underweight3,
    /// <summary> Bmi Underweight 2 </summary>
    Underweight2,
    /// <summary> Bmi Underweight 1 </summary>
    Underweight1,
    /// <summary> Bmi Normal </summary>
    Normal,
    /// <summary> Bmi Overweight </summary>
    Overweight,
    /// <summary> Bmi Obese 1</summary>
    Obese1,
    /// <summary> Bmi Obese 2 </summary>
    Obese2,
    /// <summary> Bmi Obese 3 </summary>
    Obese3,
}

/// <summary>
///     Client Class
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
            < 3.5 => throw new InvalidClientDataException("BMI not valid, too small"),
            < 16 => Bmi.Underweight3,
            < 17 => Bmi.Underweight2,
            < 18.5 => Bmi.Underweight1,
            < 25 => Bmi.Normal,
            < 30 => Bmi.Overweight,
            < 35 => Bmi.Obese1,
            < 40 => Bmi.Obese2,
            < 100 => Bmi.Obese3,
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
    ///     Register a new client.
    ///     Client's class constructor.
    /// </summary>
    /// <param name="firstName"> First Name </param>
    /// <param name="lastName"> Last Name</param>
    /// <param name="gender"> Gender </param>
    /// <param name="dateOfBirth"> Date Of Birth </param>
    /// <param name="nif"> Nif </param>
    /// <param name="address"> Address </param>
    /// <param name="email"> Email </param>
    /// <param name="loginData"></param>
    public Client(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  LoginData loginData)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email,
               loginData)
    { }

    #endregion

    #region methods
    
    /// <summary>
    ///     The client's height
    /// </summary>
    /// <param name="height"> The client's height</param>
    /// <exception cref="InvalidClientDataException">Invalid Data</exception>
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
    ///     The client's weight.
    /// </summary>
    /// <param name="weight"> The Client's weight </param>
    /// <exception cref="InvalidClientDataException">Invalid Data</exception>
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
    ///     Method to run when a client enters the gym.
    /// </summary>
    /// <returns> Session status </returns>
    internal Session GymEntrance() =>
        subscription != null && subscription.Status != SubscriptionStatus.Inactive
            ? Session.RegisteredEntry
            : Session.UnAuthorized;

    /// <summary>
    ///     Get client's Imc history
    /// </summary>
    /// <returns>Imc history. </returns>
    internal static List<double>? GetImcHistory() { return default; }

    /// <summary>
    ///     Create an Instance of client and insert it to the database.
    /// </summary>
    /// <param name="firstName"> First Name </param>
    /// <param name="lastName"> Last Name</param>
    /// <param name="gender"> Gender </param>
    /// <param name="dateOfBirth"> Date Of Birth </param>
    /// <param name="nif"> Nif </param>
    /// <param name="address"> Address </param>
    /// <param name="email"> Email </param>
    /// <param name="username"> username </param>
    /// <param name="hashedPassword"> Hashed password </param>
    /// <param name="twoFactorAuth"> two factor auth </param>
    /// <returns> An Instance of client </returns>
    /// <exception cref="InvalidUserException"></exception>
    internal static async Task<Client?>
    NewClientAsync(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  string username, string hashedPassword, string twoFactorAuth
                  )
    {
        try
        {
            var loginData = await LoginData.NewUserAsync(
                username, hashedPassword, twoFactorAuth, DateTime.Now, UserType.Client);

            if (loginData == null)
                throw new InvalidUserException(
                    "Cannot insert invalid user, or duplicate");
            var client = new Client(firstName, lastName, gender, dateOfBirth, nif,
                address, email, loginData);
            // insert to database.
            await client.InsertUserDataToDbAsync(username);
            await Address.InsertToDbAsync(address, username);

            return client;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///     Get an instance of client with a username asynchronously.
    /// </summary>
    /// <param name="username"> username </param>
    /// <returns> An instance of client </returns>
    /// <exception cref="InvalidUserException"> Invalid username</exception>
    internal static async Task<Client?> GetWithUsernameAsync(string username)
    {
        try
        {
            var values = await CmdExecuteQuerySingleAsync(
                $"select * from userdata where logindatausername='{username}' and " +
                $"((select (usertype) from logindata where username = '{username}') = 2);");

            string? firstName = default;
            string? lastName = default;
            DateTime birthDate = default;
            DateTime userSince = default;
            Gender gender = default;
            int nif = default;
            string? email = default;
            int? phone = default;

            foreach (var val in from column in values
                                where values is not null
                                where column.Value is not System.DBNull
                                select column)
                switch (val.Key)
                {
                    case 1:
                        firstName = (string)val.Value;
                        break;
                    case 2:
                        lastName = (string)val.Value;
                        break;
                    case 3:
                        birthDate = (DateTime)val.Value;
                        break;
                    case 4:
                        gender = (Gender)val.Value;
                        break;
                    case 5:
                        nif = (int)val.Value;
                        break;
                    case 6:
                        phone = (int)val.Value;
                        break;
                    case 7:
                        userSince = (DateTime)val.Value;
                        break;
                }

            Address? address = await Address.GetWithUsernameAsync(username);

            LoginData? loginData =
                await LoginData.GetWithUsernameAsync(username);

            if (loginData == null || firstName == null || lastName == null ||
                address == null)
                throw new InvalidUserException("Invalid data");

            return new Client(firstName, lastName, gender, birthDate, nif,
                              address, email, loginData);
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///     Get all clients stored on the database server.
    /// </summary>
    /// <returns> A list of clients </returns>
    /// <exception cref="InvalidUserException"></exception>
    internal static async Task<List<Client>?> GetAll()
    {
        try
        {
            var values = await CmdExecuteQueryAsync(
                "select * from userdata where ((select (usertype) from logindata) = 2);");

            var list = new List<Client>();

            foreach (var line in from line in values
                                 where values is not null
                                 select line)
            {
                string? username = default;
                string? firstName = default;
                string? lastName = default;
                DateTime birthDate = default;
                DateTime userSince = default;
                Gender gender = default;
                int nif = default;
                string? email = default;
                int? phone = default;

                foreach (var val in from column in line
                                    where line is not null
                                    where column.Value is not System.DBNull
                                    select column)
                    switch (val.Key)
                    {
                        case 0:
                            username = (String)val.Value;
                            break;
                        case 1:
                            firstName = (String)val.Value;
                            break;
                        case 2:
                            lastName = (String)val.Value;
                            break;
                        case 3:
                            birthDate = (DateTime)val.Value;
                            break;
                        case 4:
                            gender = (Gender)val.Value;
                            break;
                        case 5:
                            nif = (int)val.Value;
                            break;
                        case 6:
                            phone = (int)val.Value;
                            break;
                        case 7:
                            userSince = (DateTime)val.Value;
                            break;
                    }
                if (username == null)
                    throw new InvalidUserException("Invalid data");

                Address? address = await Address.GetWithUsernameAsync(username);

                LoginData? loginData =
                    await LoginData.GetWithUsernameAsync(username);

                if (loginData == null || loginData == null ||
                    firstName == null || lastName == null || address == null)
                    throw new InvalidUserException("Invalid data");

                list.Add(new Client(firstName, lastName, gender, birthDate, nif,
                                    address, email, loginData));
            }
            return list;
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///     LogIn for client.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogIn() { throw new NotImplementedException(); }

    /// <summary>
    ///     LogOut For client
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogOut() => throw new NotImplementedException();
    

    #endregion
}
