using System.Text.Json;
using System.Text.Json.Serialization;
using static Data.DataBase;

namespace Host;

/// <summary>
/// </summary>
internal enum Gender
{
    Unspecified,
    Female,
    Male,
}

/// <summary>
///
/// </summary>
internal abstract class Person : Gym
{
    // internal Person(string firstName, string lastName, Gender gender,
    //                 DateTime dateOfBirth, ulong nif, Address address)
    // {
    //     FirstName = firstName;
    //     LastName = lastName;
    //     Gender = gender;
    //     DateOfBirth = dateOfBirth;
    //     Nif = nif;
    //
    //     Addresses = new List<Address>();
    //     Addresses.Add(address);
    // }

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
    /// The user's nif.
    /// </summary>
    public ulong Nif { get; private set; }

    public List<Address> Addresses { get; private set; }

    /// <summary>
    /// When the user registered the account.
    /// </summary>
    private protected DateTime? RegisteredDate { get; set; }

    #region methods

    #endregion

    #region abstract_methods

    abstract private protected Task InsertUser(Person user);

    #endregion
}
