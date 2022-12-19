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
[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class PersonJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Client))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class ClientJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    ///     The user's first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///     The user's last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     The user's gender.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     The user's date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// The user's nif.
    /// </summary>
    public uint Nif { get; set; }

    // /// <summary>
    // /// The user's address.
    // /// </summary>
    // private protected string? Address { get; set; }

    /// <summary>
    /// When the user registered the account.
    /// </summary>
    private protected DateTime? RegisteredDate { get; set; }
    #region methods

    /// <summary>
    ///     Change the user's address.
    /// </summary>
    /// <param name="addr">the user's new address</param>
    // private protected void ChangeAddress(string? addr) => Address = addr;

    #endregion

    #region abstract_methods

    abstract private protected Task InsertUser(Person user);

    #endregion
}
