using static Data.DataBase;
using static Utils.Logger;

using Host.Login;
using Data;

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
                    DateTime dateOfBirth, int nif, Address address,
                    string email, LoginData loginData)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Nif = nif;
        Email = email;

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
    public Gender Gender { get; private set; }

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
    public int Phone { get; private set; } = 900000000;

    /// <summary>
    ///     The user's email.
    /// </summary>
    public string Email { get; private set; }

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
            Console.WriteLine(DateOfBirth);

            await CmdExecuteNonQueryAsync(
                $"insert into userdata(logindatausername,firstname,lastname,birthdate,gender,nif,phone,usersince)" +
                $" VALUES ('{username}','{FirstName}','{LastName}', '{DateOfBirth}', {(int)Gender}, {Nif}," +
                $" {Phone}, (SELECT NOW()) );");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    #endregion
}
