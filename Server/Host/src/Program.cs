using Host;
using static Utils.Logger;

LoggerInit();

try
{
    //await DataBase.DataBase.CmdExecuteNonQueryAsync(@$"insert into
    //         logindata(username,hashedpassword,lastlogin) VALUES ('db3',
    //        'asd;flkj',SELECT NOW())");

    var client1 = new Client();

    //var name = Client.FirstName;

    ILogin l = client1;

    var hash = await l.PassHash;

    Log.Info(hash);

    var val = await DataBase.DataBase.CmdExecuteQueryAsync<DateTime>
        ("SELECT lastlogin From logindata WHERE username='db4'");

    Log.Info(val);
}
catch (Exception e)
{
    Log.Error(e);
}