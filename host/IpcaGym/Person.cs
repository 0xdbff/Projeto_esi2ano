namespace host;

public abstract class Person
{
    public uint Age { get; set; }
    public string? Name { get; set; }

    #region private_attributes
    
    private uint _nif;
    private string? _address;
    private bool _loginStatus;
    private string? _hashedPassword;
    private string? _registeredDate;
    
    #endregion
    
    #region methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addr"></param>
    private void ChangeAddress(string? addr)
        => _address = addr;

    #endregion

    #region abstract_methods
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract bool Login();
 
    #endregion
}