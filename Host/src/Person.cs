using Host;

namespace host;

public enum Gender
{
    Male,
    Female,
    Unspecified,
}

/// <summary>
/// 
/// </summary>
public abstract class Person
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
    public Gender gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateOnly DateOfBirth { get; set; }


    #region methods

    /// <summary>
    /// </summary>
    /// <param name="addr"></param>
    private void ChangeAddress(string? addr)
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
    private uint _nif;

    /// <summary>
    /// 
    /// </summary>
    private string? _address;

    /// <summary>
    /// 
    /// </summary>
    private bool _loginStatus;

    /// <summary>
    /// 
    /// </summary>
    private string? _hashedPassword;

    /// <summary>
    /// 
    /// </summary>
    private string? _registeredDate;

    #endregion
}