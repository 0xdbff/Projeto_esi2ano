using Host.Login;
using Host;

using static Utils.Logger;
using static Utils.Security;

LoggerInit();

await Data.DataBase.Init();

await Admin.Init();

var admin = await Admin.GetWithUsername("IpcaGymAdmin");

if (admin != null)
    Console.WriteLine(admin.Address.ToString());

var addr1 = Address.GenExample1();

var cl1 = await Client.NewClientAsync(
    "test", "test", Gender.Male, new DateTime(2003, 2, 3), 1000000, addr1,
    "email", "username2", SHA512(DateTime.Now.ToString()), " ");

var cl2 = await Client.GetWithUsernameAsync("username2");
if (cl2 != null)
    //
    Console.WriteLine(cl2.FirstName);
