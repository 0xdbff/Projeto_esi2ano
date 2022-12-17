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

// [JsonSerializable(typeof(Person))]
// [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
//                              PropertyNamingPolicy =
//                                  JsonKnownNamingPolicy.CamelCase,
//                              WriteIndented = true)]
// internal partial class PersonJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    ///     The user's first name.
    /// </summary>
    internal string? FirstName { get; set; }

    /// <summary>
    ///     The user's last name.
    /// </summary>
    internal string? LastName { get; set; }

    /// <summary>
    ///     The user's gender.
    /// </summary>
    internal Gender Gender { get; set; }

    /// <summary>
    ///     The user's date of birth.
    /// </summary>
    internal DateTime DateOfBirth { get; set; }

    #region methods

    /// <summary>
    ///     Change the user's address.
    /// </summary>
    /// <param name="addr">the user's new address</param>
    private protected void ChangeAddress(string? addr) => _address = addr;

    #endregion

    #region abstract_methods

    // abstract private protected Task InsertUser(UserData user);

    #endregion

    #region private_attributes

    /// <summary>
    /// The user's nif.
    /// </summary>
    private protected uint _nif;

    /// <summary>
    /// The user's address.
    /// </summary>
    private protected string? _address;

    /// <summary>
    /// When the user registered the account.
    /// </summary>
    private protected DateTime? RegisteredDate { get; set; }

    #endregion
}
