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
    public Admin() { }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    ILogin.LoginStatus ILogin.Login() { throw new NotImplementedException(); }
}
