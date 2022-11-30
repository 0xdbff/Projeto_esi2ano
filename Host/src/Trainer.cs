using Host;

namespace host;

/// <summary>
/// 
/// </summary>
public class Trainer : Person, ILogin
{
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