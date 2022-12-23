namespace Host.Login;

/// <summary>
/// Interface Login
/// </summary>
internal interface ILogin
{
    #region methods

    /// <summary>
    ///     LogIn for a user that implements Ilogin.
    /// </summary>
    /// <returns></returns>
    private protected LoginStatus LogIn();

    /// <summary>
    ///     LogOut for a user that implements Ilogin.
    /// </summary>
    /// <returns></returns>
    private protected LoginStatus LogOut();

    /// <summary>
    ///     Get login history for a user that implements Ilogin.
    /// </summary>
    /// <returns></returns>
    internal static List<LoginAttempt>? GetLoginHistory() { return default; }

    #endregion
}