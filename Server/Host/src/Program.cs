using Host.Login;
using Host;

using static Utils.Logger;

LoggerInit();

// Gym ipcaGym = await Gym.

// var test = new Dictionary<int, object>();

// var addr = Address.GenExample1();
//
// await addr.InsertToDb("db8");
//
// var addr2 = await Address.GetWithUsername("db8");
//
// if (addr2 != null)
// {
//     Console.WriteLine(addr2.Code);
//     Console.WriteLine(addr2.Localidade);
//     Console.WriteLine(addr2.HouseNum);
//     Console.WriteLine(addr2.AdditionalInfo);
//     Console.WriteLine(addr2.LastUpdate);
// }

await Data.DataBase.Init();

var admin = await Admin.GetWithUsername("admin");

if (admin != null)
{
    Console.WriteLine(admin.FirstName);
    Console.WriteLine(admin.LastName);
    Console.WriteLine(admin.Gender);
    Console.WriteLine(admin.DateOfBirth);
    Console.WriteLine(admin.LoginData.Username);
    Console.WriteLine(admin.LoginData.HashedPassword);

    //
}

// await Admin.insertDefaultAdmin();

// var l = await LoginData.GetAll();
// // var val = await LoginData.GetWithUsername("db8");
// if (l != null)
// {
//     foreach (var val in l)
//     {
//         Console.WriteLine(val.Username);
//         Console.WriteLine(val.HashedPassword);
//         Console.WriteLine(val.TwoFactorAuth);
//         Console.WriteLine(val.LastLogin);
//     }
// }

// await Client.test();

// var ipcaGym = new Gym();

// ipcaGym.Test();

// await Invoice.GenerateInvoicePdf();
