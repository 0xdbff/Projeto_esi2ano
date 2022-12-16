using Host;
using static Utils.Logger;
using static Utils.Security;

LoggerInit();

try
{
    var client1 = new Client();

    var passwordQueOClientPos = "123456789fF";

    var hash = SHA512(passwordQueOClientPos);

    // await DataBase.DataBase.CmdExecuteNonQueryAsync(@$"insert into
    //         logindata(username,hashedpassword,lastlogin) VALUES ('db8',
    //        '{hash}',(SELECT NOW()))");
    //
    //var name = Client.FirstName;

    var val = await DataBase.DataBase.CmdExecuteQueryAsync<DateTime>
        ("SELECT lastlogin From logindata WHERE username='db8'");

    var val2 = await DataBase.DataBase.CmdExecuteQueryAsync<string>
        ("SELECT hashedpassword From logindata WHERE username='db8'");

    Console.WriteLine(val);

    Console.WriteLine(val2);

    Console.WriteLine(hash);

    Console.WriteLine($"Veracidade da palavra passe {hash == val2}");
}
catch (Exception e)
{
    Log.Error(e);
}
