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

    private protected override Task InsertUser(Person user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.Login() { throw new NotImplementedException(); }
}
