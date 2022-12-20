using System.Net;
using System.Text.Json;
using Host;

using static Utils.Logger;
using static Utils.Security;

internal class Program
{
    private static async Task Main(string[] args)
    {
        LoggerInit();

        try
        {
            await Client.Example1();

            // // await DataBase.DataBase.CmdExecuteNonQueryAsync(@$"insert
            // into
            // //         logindata(username,hashedpassword,lastlogin) VALUES
            // ('db8',
            // //        '{hash}',(SELECT NOW()))");
            // //
            // // var name = Client.FirstName;
            //
            // var val = await Data.DataBase.CmdExecuteQueryAsync<DateTime>(
            //     "SELECT lastlogin From logindata WHERE username='db8'");
            //
            // var val2 = await Data.DataBase.CmdExecuteQueryAsync<string>(
            //     "SELECT hashedpassword From logindata WHERE
            // username='db8'");
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }
}
