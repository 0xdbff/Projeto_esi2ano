using Host.Login;
using Host.Exceptions;
using Data;

using static Data.DataBase;
using static Utils.Logger;
using static Utils.Security;

namespace Host;

/// <summary>
///     Admin class.
/// </summary>
internal class Admin : Person, ILogin
{
    /// <summary>
    ///     A struct to perform analysis by month.
    ///     All instance will be static. 
    /// </summary>
    private struct AnalysisByMonth
    {
        /// <summary>
        ///     Month of instance.
        /// </summary>
        private DateOnly _month;

        /// <summary>
        ///     all expenses of the month.
        /// </summary>
        private double _expenses;

        /// <summary>
        ///     all incomes of the month.
        /// </summary>
        private double _income;

        /// <summary>
        ///     Constructor of analysis by month
        /// </summary>
        /// <param name="month"> month of instance </param>
        /// <param name="expenses"> expenses month </param>
        /// <param name="income"> incomes for month </param>
        public AnalysisByMonth(DateOnly month, double expenses, double income)
        {
            this._month = month;
            this._expenses = expenses;
            this._income = income;
        }
    }

    /// <summary>
    ///     A struct to perform analysis by month.
    /// </summary>
    private static List<AnalysisByMonth>? analysis;


    /// <summary>
    ///     Admin constructor.
    /// </summary>
    /// <param name="firstName"> First name </param>
    /// <param name="lastName"> Last name </param>
    /// <param name="gender"> Gender </param>
    /// <param name="dateOfBirth"> Date of birth </param>
    /// <param name="nif"> Nif </param>
    /// <param name="address"> address </param>
    /// <param name="email"> email </param>
    /// <param name="loginData"> login data </param>
    private Admin(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  LoginData loginData)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email,
               loginData)
    { }

    /// <summary>
    ///     Create a new admin and insert to database asynchronously.  
    /// </summary>
    /// <param name="firstName"> First name </param>
    /// <param name="lastName"> Last name </param>
    /// <param name="gender"> Gender </param>
    /// <param name="dateOfBirth"> Date of birth </param>
    /// <param name="nif"> Nif </param>
    /// <param name="address"> address </param>
    /// <param name="email"> email </param>
    /// <param name="username"> username </param>
    /// <param name="hashedPassword"> hashed password </param>
    /// <param name="twoFactorAuth"> two factor auth app </param>
    /// <returns>An instance of Admin</returns>
    /// <exception cref="InvalidUserException"></exception>
    internal static async Task<Admin?>
    NewAdminAsync(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  string username, string hashedPassword, string twoFactorAuth
                  )
    {
        try
        {
            var loginData = await LoginData.NewUserAsync(
                username, hashedPassword, twoFactorAuth, DateTime.Now,UserType.Admin);

            if (loginData != null)
            {
                var admin = new Admin(firstName, lastName, gender, dateOfBirth, nif,
                                      address, email, loginData);
                // insert to database.
                await admin.InsertUserDataToDbAsync(username);
                await Address.InsertToDbAsync(address, username);

                return admin;
            }
            throw new InvalidUserException(
                "Cannot insert invalid user, or duplicate");
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///     Insert a default Administrator if one does not already exists.
    /// </summary>
    internal static async Task Init()
    {
        try
        {
            if (await GetWithUsername("IpcaGymAdmin") != null)
                return;
            else
                await InsertDefaultAdmin();
        }
        catch (Exception e)
        {
            Log.Error(e);
            Environment.Exit(1);
        }
    }

    /// <summary>
    ///     Insert default Administrator asynchronously.
    /// </summary>
    /// <returns> An awaitable Task </returns>
    private static async Task
    InsertDefaultAdmin() => await Admin.NewAdminAsync(
        "default", "admin", 0, new DateTime(1999, 1, 1), 10000000,
        new Address("4750-810", "Portugal", "Barcelos", DateTime.Now,
                    "Lugar do Aldão", 0, "Vila Frescainha (São Martinho)"),
        "admin@ipcagym.org", "IpcaGymAdmin", SHA512($"IpcaGymPa$$word!"), ""
        );

    /// <summary>
    ///     try Get an instance of Admin with a username.
    /// </summary>
    /// <param name="username"> username </param>
    /// <returns> And instance of Admin or null</returns>
    /// <exception cref="InvalidUserException"></exception>
    internal static async Task<Admin?> GetWithUsername(string username)
    {
        try
        {
            var values = await CmdExecuteQuerySingleAsync(
                $"select * from userdata where logindatausername='{username}' and " +
                $"((select (usertype) from logindata where username = '{username}') = 0);");

            string? firstName = default;
            string? lastName = default;
            DateTime birthDate = default;
            DateTime userSince = default;
            Gender gender = default;
            int nif = default;
            string? email = default;
            int? phone = default;

            foreach (var val in from column in values
                                where values is not null
                                where column.Value is not System.DBNull
                                select column)
                switch (val.Key)
                {
                    case 1:
                        firstName = (string)val.Value;
                        break;
                    case 2:
                        lastName = (string)val.Value;
                        break;
                    case 3:
                        birthDate = (DateTime)val.Value;
                        break;
                    case 4:
                        gender = (Gender)val.Value;
                        break;
                    case 5:
                        nif = (int)val.Value;
                        break;
                    case 6:
                        phone = (int)val.Value;
                        break;
                    case 7:
                        userSince = (DateTime)val.Value;
                        break;
                }

            var address = await Address.GetWithUsernameAsync(username);

            var loginData =
                await LoginData.GetWithUsernameAsync(username);

            if (loginData == null || loginData == null || firstName == null ||
                lastName == null || address == null)
                throw new InvalidUserException("Invalid data");

            return new Admin(firstName, lastName, gender, birthDate, nif,
                             address, email, loginData);
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
    ///     Get a list of all Admins.
    /// </summary>
    /// <returns> A list with all users stored on database. </returns>
    /// <exception cref="InvalidUserException"> Invalid Data .</exception>
    internal static async Task<List<Admin>?> GetAll()
    {
        try
        {
            var values = await CmdExecuteQueryAsync(
                "select * from userdata where ((select (usertype) from logindata) = 0);");
            var list = new List<Admin>();

            foreach (var line in from line in values
                                 where values is not null
                                 select line)
            {
                string? username = default;
                string? firstName = default;
                string? lastName = default;
                DateTime birthDate = default;
                DateTime userSince = default;
                Gender gender = default;
                int nif = default;
                string? email = default;
                int? phone = default;

                foreach (var val in from column in line
                                    where line is not null
                                    where column.Value is not System.DBNull
                                    select column)
                    switch (val.Key)
                    {
                        case 0:
                            username = (String)val.Value;
                            break;
                        case 1:
                            firstName = (String)val.Value;
                            break;
                        case 2:
                            lastName = (String)val.Value;
                            break;
                        case 3:
                            birthDate = (DateTime)val.Value;
                            break;
                        case 4:
                            gender = (Gender)val.Value;
                            break;
                        case 5:
                            nif = (int)val.Value;
                            break;
                        case 6:
                            phone = (int)val.Value;
                            break;
                        case 7:
                            userSince = (DateTime)val.Value;
                            break;
                    }
                if (username == null)
                    throw new InvalidUserException("Invalid data");

                Address? address = await Address.GetWithUsernameAsync(username);

                LoginData? loginData =
                    await LoginData.GetWithUsernameAsync(username);

                if (loginData == null || loginData == null ||
                    firstName == null || lastName == null || address == null)
                    throw new InvalidUserException("Invalid data");

                list.Add(new Admin(firstName, lastName, gender, birthDate, nif,
                                   address, email, loginData));
            }
            return list;
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
    ///     Login for Admin.
    /// </summary>
    /// <returns> Login Status </returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogIn() { throw new NotImplementedException(); }

    /// <summary>
    ///     Logout for Admin
    /// </summary>
    /// <returns> Login Status </returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogOut() { throw new NotImplementedException(); }
}
