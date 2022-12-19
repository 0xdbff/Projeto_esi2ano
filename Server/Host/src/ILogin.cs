using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

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

    /// <summary>
    ///
    /// </summary>
    internal class LoginAttempt
    {
        /// <summary>
        ///
        /// </summary>
        public AuthType AuthType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? Authentication { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? HashedPassword { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? AuthCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public LoginStatus loginStatus { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public enum AuthType
    {
        /// <summary> </summary>
        Email,
        /// <summary> </summary>
        Phone,
        /// <summary> </summary>
        UserName,
    }

    /// <summary>
    ///
    /// </summary>
    internal class Data
    {
        /// <summary>
        ///
        /// </summary>
        public string? Username { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string? HashedPassword { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public bool TwoFactorAuth { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime LastLogin { get; private set; }
    }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(LoginAttempt))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class LoginAttemptJsonContext : JsonSerializerContext { }

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
    internal static List<LoginAttempt>? GetCredentialsHistory()
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
