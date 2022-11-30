using Host;

namespace Host;

/// <summary>
/// 
/// </summary>
public enum ClientType
{
    Academic,
    Common,
    Invalid
}

/// <summary>
/// 
/// </summary>
internal class Client : Person, ILogin
{
    /// <summary>
    /// Register a new client.
    /// Client's class constructor.
    /// </summary>
    public Client()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    List<ILogin.UsedCredentials> ILogin.GetCredentialsHistory()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    List<ILogin.Data> ILogin.GetLoginHistory()
    {
        throw new NotImplementedException();
    }

    #region methods

    #endregion
}