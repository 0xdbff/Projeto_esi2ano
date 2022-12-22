using static Data.DataBase;

namespace Host.Login;

/// <summary>
///
/// </summary>
internal interface ILogin
{
    #region data

    /// <summary>
    ///
    /// </summary>
    internal Task<string?> PassHashGetAsync
    {
        get => CmdExecuteQueryAsync<string>(
            "SELECT hashedpassword FROM logindata WHERE username='db4'");
    }

    #endregion

    #region methods

    private protected async Task<int> CreateNewUserAsync(string username,
                                                         string passwordHash)
    {
        try
        {
            return await CmdExecuteNonQueryAsync(
                $"INSERT INTO logindata(username,hashedpassword) VALUES" +
                $"('{username}','{passwordHash}')");
        }
        catch
        {
            throw new Exception("User already exists");
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private protected LoginStatus LogIn();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private protected LoginStatus LogOut();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    internal static List<LoginAttempt>? GetLoginHistory() { return default; }

    #endregion
}
