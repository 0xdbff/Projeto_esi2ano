using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal enum UserType
{
    /// <summary> </summary>
    Admin,
    /// <summary> </summary>
    Trainer,
    /// <summary> </summary>
    Client
}

/// <summary>
///
/// </summary>
internal enum IpType { v4, v6 }

/// <summary>
///
/// </summary>
internal struct Ip
{
    internal IpType Type { get; set; }
}

/// <summary>
///
/// </summary>
internal interface ILogin
{
    /// <summary>
    ///
    /// </summary>
    public enum AuthType
    {
        /// <summary> </summary>
        Email,
        /// <summary> </summary>
        UserName,
    }

    /// <summary>
    ///
    /// </summary>
    internal struct UsedCredentials
    {
        /// <summary>
        ///
        /// </summary>
        internal AuthType AuthType { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal string? HashedPassword { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string? AuthCode { get; private set; }
    }

    /// <summary>
    ///
    /// </summary>
    public enum LoginStatus
    {
        /// <summary> </summary>
        LoggedIn,
        /// <summary> </summary>
        LoggedOut,
        /// <summary> </summary>
        DeviceLimitViolated,
        /// <summary> </summary>
        InvalidCredentials,
        /// <summary> </summary>
        UnauthoryzedLocation,
        /// <summary> </summary>
        WaitingForAuthCode,
        /// <summary> </summary>
        AuthCodeExpired,
    }

    #region data

    /// <summary>
    ///
    /// </summary>
    internal struct Data
    {
        /// <summary>
        ///
        /// </summary>
        internal readonly string? Username;

        /// <summary>
        ///
        /// </summary>
        internal readonly string? HashedPassword;

        /// <summary>
        ///
        /// </summary>
        internal readonly bool TwoFactorAuth;

        /// <summary>
        ///
        /// </summary>
        internal readonly DateTime LastLogin;
    }

    /// <summary>
    ///
    /// </summary>
    internal Task<string?> PassHashGetAsync
    {
        get => CmdExecuteQueryAsync<string>(
            "SELECT hashedpassword From logindata WHERE username='db4'");
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
    private protected LoginStatus Login();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    internal static List<UsedCredentials>? GetCredentialsHistory()
    {
        return default;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    internal static List<Data>? GetLoginHistory() { return default; }

    #endregion
}
