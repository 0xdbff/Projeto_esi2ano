namespace Host;

/// <summary>
/// </summary>
internal enum Gender
{
    Male,
    Female,
    Unspecified,
}

/// <summary>
///     An abstract class
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    ///     the user's first name.
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
    internal DateOnly DateOfBirth { get; set; }

    #region methods

    /// <summary>
    ///     Change the user's address.
    /// </summary>
    /// <param name="addr">the user's new address</param>
    private protected void ChangeAddress(string? addr) => _address = addr;

    #endregion

    #region abstract_methods

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
