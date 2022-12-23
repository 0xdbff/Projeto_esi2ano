using Host.Login;
using Host.Exceptions;
using Data;

using static Data.DataBase;
using static Utils.Logger;
using static Utils.Security;

namespace Host;

/// <summary>
///
/// </summary>
internal class Admin : Person, ILogin
{
    /// <summary>
    ///
    /// </summary>
    private struct analyisByMonth
    {
        /// <summary>
        ///
        /// </summary>
        DateOnly month;

        /// <summary>
        ///
        /// </summary>
        double expenses;

        /// <summary>
        ///
        /// </summary>
        double income;
    }

    /// <summary>
    ///
    /// </summary>
    private static List<analyisByMonth>? analysis;

    /// <summary>
    ///
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="gender"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="nif"></param>
    /// <param name="address"></param>
    private Admin(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  LoginData loginData)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email,
               loginData)
    { }

    internal static async Task<Admin?>
    NewAdminAsync(string firstName, string lastName, Gender gender,
                  DateTime dateOfBirth, int nif, Address address, string email,
                  string username, string hashedPassword, string twoFactorAuth)
    {
        try
        {
            var loginData = await LoginData.NewUserAsync(username, hashedPassword,
                                                         twoFactorAuth, DateTime.Now);

            if (loginData != null)
            {
                var admin = new Admin(firstName, lastName, gender, dateOfBirth, nif,
                                      address, email, loginData);
                // insert to database.
                await admin.InsertUserDataToDbAsync(username);

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

    internal static async Task insertDefaultAdmin() => await Admin.NewAdminAsync(
        "default", "admin", 0, new DateTime(1999, 1, 1), 10000000, new Address(),
        "admin@ipcagym.org", "IpcaGymAdmin", SHA512($"IpcaGymPa$$word!"), "");

    internal static async Task<Admin?> GetWithUsername(string username)
    {
        try
        {
            var values = await CmdExecuteQuerySingleAsync(
                $"SELECT * from userdata WHERE username = '{username}' LIMIT 1;");

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

            Address? address = await Address.GetWithUsername(username);

            LoginData? loginData =
                await LoginData.GetWithUsernameAsync(username);

            if (address == null || loginData == null || firstName == null ||
                lastName == null || email == null)
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
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogIn() { throw new NotImplementedException(); }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogOut() { throw new NotImplementedException(); }
}
