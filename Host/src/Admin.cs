using Host;

namespace Host;

/// <summary>
/// 
/// </summary>
internal class Admin : Person, ILogin
{
    /// <summary>
    /// 
    /// </summary>
    public Admin()
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