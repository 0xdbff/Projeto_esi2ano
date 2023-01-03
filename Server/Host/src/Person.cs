using static Data.DataBase;
using static Utils.Logger;

using Host.Login;
using Data;
using Host.Exceptions;

namespace Host;

/// <summary>
///     Gender type
/// </summary>
internal enum Gender
{
    /// <summary>
    ///     Unspecified
    /// </summary>
    Unspecified,
    /// <summary>
    ///     Female
    /// </summary>
    Female,
    /// <summary>
    ///     Male
    /// </summary>
    Male,
}

/// <summary>
///     Email type.
/// </summary>
internal enum EmailType
{
    /// <summary>
    ///     Academic email.
    /// </summary>
    Academic,
    /// <summary>
    ///     Normal email.
    /// </summary>
    Normal,
    /// <summary>
    ///     Gym email.
    /// </summary>
    GymEmail,
}

/// <summary>
///     Person Class
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    ///     Person constructor
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="gender"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="nif"></param>
    /// <param name="address"></param>
    internal Person(string firstName, string lastName, Gender gender,
                    DateTime dateOfBirth, int nif, Address address, string email,
                    EmailType emailType, LoginData loginData)
    {
        FirstName = firstName;
        LastName = lastName;
        UserGender = gender;
        DateOfBirth = dateOfBirth;
        Nif = nif;
        Email = email;
        EmailType = emailType;

        LoginData = loginData;
        Address = address;
    }

    /// <summary>
    ///     The user's login data.
    /// </summary>
    public LoginData LoginData { get; private set; }

    /// <summary>
    ///     The user's Full name.
    /// </summary>
    public string Name { get => FirstName + " " + LastName; }

    /// <summary>
    ///     The user's First name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    ///     The user's last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    ///     The user's gender.
    /// </summary>
    public Gender UserGender { get; private set; }

    /// <summary>
    ///     The user's date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    ///     The user's nif.
    /// </summary>
    public int Nif { get; private set; }

    /// <summary>
    ///     The user's phone.
    /// </summary>
    public int Phone
    {
        get; private set;
    } = new Random().Next(900000000, 999999999);

    /// <summary>
    ///     The user's email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    ///     The user's email.
    /// </summary>
    public EmailType EmailType { get; private set; }

    /// <summary>
    ///     The User's address.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    ///     When the user registered the account.
    /// </summary>
    private protected DateTime? RegisteredDate { get; set; }

    #region methods

    #endregion

    #region abstract_methods

    /// <summary>
    ///     Insert to database asynchronously.
    /// </summary>
    /// <param name="username">username</param>
    private protected async Task InsertUserDataToDbAsync(string username)
    {
        try
        {
            await CmdExecuteNonQueryAsync(
                $"insert into userdata(logindatausername,firstname,lastname,birthdate,gender,nif,phone,usersince)" +
                $" VALUES ('{username}','{FirstName}','{LastName}', '{DateOfBirth.ToString("yyyy-M-d")}', '{UserGender.ToString()}', {Nif}," +
                $" {Phone}, (SELECT NOW()) );");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///    Add email data to database asynchronously.
    /// </summary>
    private protected async Task InsertEmailDataToDbAsync(string username, EmailType emailType)
    {
        try
        {
            await CmdExecuteNonQueryAsync(
                $"INSERT into emailinfo(email, logindatausername, validated ,emailtypetype, subscrivedtonews) VALUES" +
                $"('{Email}', '{username}' , 1 , {(int)EmailType} , true);");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///    Get email data with a username.
    /// </summary>
    internal static async Task<(string, EmailType)>
    GetEmailWithUsername(string username)
    {
        try
        {
            var values =
                await CmdExecuteQuerySingleAsync("select * from emailinfo where " +
                                                 $"logindatausername = '{username}'");

            string? email = default;
            EmailType? emailType = default;

            foreach (var val in from column in values
                                where values is not null
                                where column.Value is not System.DBNull
                                select column)
                switch (val.Key)
                {
                    case 0:
                        email = (string)val.Value;
                        break;
                    case 3:
                        emailType = (EmailType)val.Value;
                        break;
                }

            if (emailType == null || email == null)
                throw new UserException("Invalid User");

            return (email, (EmailType)emailType);

        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
    }

    #endregion
}
