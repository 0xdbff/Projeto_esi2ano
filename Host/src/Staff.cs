using Host;

namespace host;

/// <summary>
/// 
/// </summary>
public class Staff : Person, ILogin
{
    /// <summary>
    /// 
    /// </summary>
    string? ILogin.Username { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// 
    /// </summary>
    string? ILogin.HashedPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// 
    /// </summary>
    DateTime ILogin.LastLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}