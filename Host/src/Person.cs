using Host;

namespace Host;

public enum Gender
{
    Male,
    Female,
    Unspecified,
}

/// <summary>
/// 
/// </summary>
internal abstract class Person : Gym
{
    /// <summary>
    /// 
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateOnly DateOfBirth { get; set; }


    #region methods

    /// <summary>
    /// </summary>
    /// <param name="addr"></param>
    private protected void ChangeAddress(string? addr)
    {
        _address = addr;
    }

    #endregion

    #region abstract_methods

    #endregion

    #region private_attributes

    /// <summary>
    /// 
    /// </summary>
    private protected uint _nif;

    /// <summary>
    /// 
    /// </summary>
    private protected string? _address;

    /// <summary>
    /// 
    /// </summary>
    private protected string? _registeredDate;

    #endregion
}