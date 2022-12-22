using Host.Login;

using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal class Admin : Person, ILogin
{
    private struct analyisByMonth
    {
        private DateOnly month;
        private double expenses;
        private double income;
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
    public Admin(string firstName, string lastName, Gender gender,
                 DateTime dateOfBirth, ulong nif, Address address, string email)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email) { }

    private static async Task insertDefaultAdmin()
    {
        await CmdExecuteNonQueryAsync("");
        //
    }

    // internal Admin(string firstName, string lastName, Gender gender,
    //                DateTime dateOfBirth)
    //{ }

    private protected override Task InsertUser(Person user)
    {
        throw new NotImplementedException();
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
