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
            // var val = new Test
            // {
            //     // FirstName = "sldkfj",
            //     LastName = "sdlfkj",
            //     age = "23",
            //     gender = "M",
            // };
            //
            // var json = JsonSerializer.Serialize(val,
            // PersonJsonContext.Default.Test);
            //
            // Console.WriteLine(json);
            //
            // var test1 =
            //     JsonSerializer.Deserialize(json, PersonJsonContext.Default.Test);
            var client1 = new Client();

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

    // Define a class to hold the data from the API response
    class ApiResponse
    {
        public string? Message { get; set; }
    }

    // internal async Task GetResponse()
    // {
    //     string response = await client.GetStringAsync(
    //         "https://jsonplaceholder.typicode.com/todos/1");
    //     Console.WriteLine(response);
    // }
}
