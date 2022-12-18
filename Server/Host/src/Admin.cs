using Host;
using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal class Admin : Person, ILogin
{
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
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    ILogin.LoginStatus ILogin.Login() { throw new NotImplementedException(); }
}
