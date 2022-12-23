using Data;
using Npgsql;

using static Data.DataBase;
using static Utils.Logger;

namespace Host.Login;

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
internal class LoginData
{
    /// <summary>
    ///
    /// </summary>
    private LoginData() { }

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
    public string? TwoFactorAuth { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime LastLogin { get; private set; }

    /// <summary>
    ///
    /// </summary>
    private static async Task<List<Dictionary<int, object?>>?>
    GetAllFromDb() => await CmdExecuteQueryAsync("SELECT * FROM logindata");

    private LoginData(string username, string hashedPassword,
                      string twoFactorAuth, DateTime lastLogin)
    {
        Username = username;
        HashedPassword = hashedPassword;
        TwoFactorAuth = twoFactorAuth;
        LastLogin = lastLogin;
    }

    internal static async Task<LoginData?> NewUserAsync(string username,
                                                        string hashedPassword,
                                                        string twoFactorAuth,
                                                        DateTime lastLogin)
    {
        try
        {
            var newUser =
                new LoginData(username, hashedPassword, twoFactorAuth, lastLogin);

            // Insert to database.
            await newUser.InsertToDbAsync();

            return newUser;
        }
        // handle duplicates and invalid insertions
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal async Task InsertToDbAsync()
    {
        try
        {
            await CmdExecuteNonQueryAsync(
                $"INSERT into logindata(username, hashedpassword, lastlogin) VALUES" +
                $"('{Username}', '{HashedPassword}' , (SELECT NOW()) );");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    internal static async Task<LoginData?> GetWithUsernameAsync(string username)
    {
        try
        {
            var values = await CmdExecuteQuerySingleAsync(
                $"SELECT * from logindata WHERE username = '{username}';");

            var data = new LoginData();

            foreach (var val in from column in values
                                where values is not null
                                where column.Value is not System.DBNull
                                select column)
                switch (val.Key)
                {
                    case 0:
                        data.Username = (String)val.Value;
                        break;
                    case 1:
                        data.HashedPassword = (String)val.Value;
                        break;
                    case 2:
                        data.TwoFactorAuth = (String)val.Value;
                        break;
                    case 3:
                        data.LastLogin = (DateTime)val.Value;
                        break;
                }

            return data;
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    internal static async Task<List<LoginData>?> GetAllAsync()
    {
        try
        {
            var values = await GetAllFromDb();
            var dataList = new List<LoginData>();

            foreach (var (line, data) in from line in values
                                         where values is not null
                                         let data = new LoginData()
                                         select (line, data))
            {
                foreach (var val in from column in line
                                    where line is not null
                                    where column.Value is not System.DBNull
                                    select column)
                {
                    switch (val.Key)
                    {
                        case 0:
                            data.Username = (String)val.Value;
                            break;
                        case 1:
                            data.HashedPassword = (String)val.Value;
                            break;
                        case 2:
                            data.TwoFactorAuth = (String)val.Value;
                            break;
                        case 3:
                            data.LastLogin = (DateTime)val.Value;
                            break;
                    }
                }
                dataList.Add(data);
            }

            return dataList;
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }
}
