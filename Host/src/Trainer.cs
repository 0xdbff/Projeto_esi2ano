using Host;

namespace Host;

/// <summary>
/// 
/// </summary>
internal class Trainer : Person, ILogin
{
    /// <summary>
    /// 
    /// </summary>
    public Trainer()
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