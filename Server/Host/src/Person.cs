using System.Text.Json;
using System.Text.Json.Serialization;
using static Data.DataBase;

namespace Host;

/// <summary>
/// </summary>
internal enum Gender
{
    /// <summary>
    ///
    /// </summary>
    Unspecified,
    /// <summary>
    ///
    /// </summary>
    Female,
    /// <summary>
    ///
    /// </summary>
    Male,
}

/// <summary>
///
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="gender"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="nif"></param>
    /// <param name="address"></param>
    internal Person(string firstName, string lastName, Gender gender,
                    DateTime dateOfBirth, ulong nif, Address address, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Nif = nif;
        Email = email;

        Addresses = new List<Address>();
        Addresses.Add(address);
    }

    public string Name {get => FirstName + " " + LastName;}

    /// <summary>
    ///     The user's first name.
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
    public ulong Nif { get; private set; }

    /// <summary>
    ///     The user's email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public List<Address> Addresses { get; private set; }

    /// <summary>
    /// When the user registered the account.
    /// </summary>
    private protected DateTime? RegisteredDate { get; set; }

    #region methods

    internal async Task AddAddress(Address address)
    {
        if (await address.IsAddressValidAsync)
            Addresses.Add(address);
    }

    #endregion

    #region abstract_methods

    private protected abstract Task InsertUser(Person user);

    #endregion
}
