using Host;

namespace host;

public class Client : Person, ILogin
{
    /// <summary>
    /// 
    /// </summary>
    string? ILogin.Username { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// 
    /// </summary>
    string? ILogin.HashedPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    DateTime ILogin.LastLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}