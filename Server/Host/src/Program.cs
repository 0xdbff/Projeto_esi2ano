using System.Net;
using System.Text.Json;
using Host;

using static Utils.Logger;
using static Utils.Security;

internal class Program
{
    HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        LoggerInit();

        try
        {
            Client.test();

            //
            // var test1 =
            //     JsonSerializer.Deserialize(json, PersonJsonContext.Default.Test);
            // var client1 = new Client
            // {
            //     FirstName = "this",
            // };
            //
            // // client1.DateOfBirth = DateTime.Now;
            // // client1.LastName = "somehing";
            //
            // var json =
            //     JsonSerializer.Serialize(client1,
            //     PersonJsonContext.Default.Person);
            //
            // Console.WriteLine(json);

            // var client1 = new Client();
            //
            // var passwordQueOClientPos = "123456789fF";
            //
            // var hash = SHA512(passwordQueOClientPos);
            //
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
            //
            // Console.WriteLine(val);
            //
            // Console.WriteLine(val2);
            //
            // Console.WriteLine(hash);
            //
            // Console.WriteLine($"Veracidade da palavra passe {hash ==
            // val2}");

            // var program = new Program();
            // await program.GetResponse();
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }
}
