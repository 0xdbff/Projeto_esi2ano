using Data;
using Npgsql;

using static Data.DataBase;
using static Utils.Logger;

namespace Host.Login;

/// <summary>
///     The User's type
/// </summary>
internal enum UserType
{
    /// <summary> Admin </summary>
    Admin,
    /// <summary> Trainer </summary>
    Trainer,
    /// <summary> Client </summary>
    Client
}

/// <summary>
///     Current logged situation for user
/// </summary>
public enum LoginStatus
{
    /// <summary> Logged in </summary>
    LoggedIn,
    /// <summary> Logged out </summary>
    LoggedOut,
    /// <summary> Device limit violated </summary>
    DeviceLimitViolated,
    /// <summary> Invalid credentials </summary>
    InvalidCredentials,
    /// <summary> unauthorized location </summary>
    UnauthoryzedLocation,
    /// <summary> waiting for authentication code </summary>
    WaitingForAuthCode,
    /// <summary> Authentication code expired </summary>
    AuthCodeExpired,
}

/// <summary>
///     Login Attemp Class
/// </summary>
internal class LoginAttempt
{
    /// <summary>
    ///     Authentication type.
    /// </summary>
    public AuthType AuthType { get; set; }

    /// <summary>
    ///     Used value in authentication.
    /// </summary>
    public string? Authentication { get; set; }

    /// <summary>
    ///     Hash for password.
    /// </summary>
    public string? HashedPassword { get; set; }

    /// <summary>
    ///     Authentication code used.
    /// </summary>
    public string? AuthCode { get; set; }

    /// <summary>
    ///     Date of Attempt.
    /// </summary>
    public DateTime date { get; set; }

    /// <summary>
    ///     Status of Attempt.
    /// </summary>
    public LoginStatus loginStatus { get; set; }
}

/// <summary>
///     Authentication method used
/// </summary>
public enum AuthType
{
    /// <summary> Email </summary>
    Email,
    /// <summary> Phone </summary>
    Phone,
    /// <summary> Username </summary>
    UserName,
}

/// <summary>
///     LoginData class
/// </summary>
internal class LoginData
{
    /// <summary>
    ///     Private constructor
    /// </summary>
    private LoginData() { }

    /// <summary>
    ///     Username
    /// </summary>
    public string? Username { get; private set; }

    /// <summary>
    ///     user's password hash.
    /// </summary>
    public string? HashedPassword { get; private set; }

    /// <summary>
    ///     user's two factor authentication app value.
    /// </summary>
    public string? TwoFactorAuth { get; private set; }

    /// <summary>
    ///     Last time logged in.
    /// </summary>
    public DateTime LastLogin { get; private set; }

    /// <summary>
    ///     user's type.
    /// </summary>
    public UserType UserType { get; set; }

    /// <summary>
    ///     Get all Login Data store on database.
    /// </summary>
    private static async Task<List<Dictionary<int, object?>>?>
    GetAllFromDb() => await CmdExecuteQueryAsync("SELECT * FROM logindata");

    private LoginData(string username, string hashedPassword,
                      string twoFactorAuth, DateTime lastLogin,
                      UserType userType)
    {
        Username = username;
        HashedPassword = hashedPassword;
        TwoFactorAuth = twoFactorAuth;
        LastLogin = lastLogin;
        UserType = userType;
    }

    /// <summary>
    ///     Login data Constructor,  asynchronously adds the instance to the
    ///     database after instantiation.
    /// </summary>
    /// <param name="username"> username </param>
    /// <param name="hashedPassword"> hashed password </param>
    /// <param name="twoFactorAuth"> two factor authentication </param>
    /// <param name="lastLogin"> last login </param>
    /// <param name="userType"> user typpe</param>
    /// <returns></returns>
    internal static async Task<LoginData?>
    NewUserAsync(string username, string hashedPassword, string twoFactorAuth,
                 DateTime lastLogin, UserType userType)
    {
        try
        {
            var newUser = new LoginData(username, hashedPassword, twoFactorAuth,
                                        lastLogin, userType);

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
    ///     Add login data to database.
    /// </summary>
    internal async Task InsertToDbAsync()
    {
        try
        {
            await CmdExecuteNonQueryAsync(
                $"INSERT into logindata(username, hashedpassword, twofactorauthapp ,lastlogin, usertype) VALUES" +
                $"('{Username}', '{HashedPassword}' , '{TwoFactorAuth}', (SELECT NOW()) , {(int)UserType});");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///     Get Login Data with username
    /// </summary>
    /// <param name="username"> username </param>
    /// <returns> A login data</returns>
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
                    case 4:
                        data.UserType = (UserType)val.Value;
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

    /// <summary>
    ///     Get all Login data on the database.
    /// </summary>
    /// <returns> A list of Login data</returns>
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
