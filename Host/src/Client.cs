using Host;

namespace host;

/// <summary>
/// 
/// </summary>
public class Client : Person, ILogin
{
    /// <summary>
    /// 
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
}