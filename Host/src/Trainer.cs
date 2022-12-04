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
    public Trainer() { }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    ILogin.LoginStatus ILogin.Login() { throw new NotImplementedException(); }
}
