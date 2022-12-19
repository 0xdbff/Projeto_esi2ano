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

    private static List<analyisByMonth>? analysis;

    private static async Task insertDefaultAdmin()
    {
        await CmdExecuteNonQueryAsync("");
        //
    }

    /// <summary>
    ///
    /// </summary>
    internal Admin() { }

    internal Admin(string firstName, string lastName, Gender gender,
                   DateTime dateOfBirth)
    { }

    private protected override Task InsertUser(Person user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.Login()
    {
        throw new NotImplementedException();
    }
}
