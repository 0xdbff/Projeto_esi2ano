using Host;
using static DataBase.DataBase;

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
    public Admin() { }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    ILogin.LoginStatus ILogin.Login() { throw new NotImplementedException(); }

}
