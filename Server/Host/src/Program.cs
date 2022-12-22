using Host.Login;

using static Utils.Logger;

LoggerInit();

var test = new Dictionary<int, object>();



var l = await LoginData.GetAll();
// var val = await LoginData.GetWithUsername("db8");
if (l != null)
{
    foreach (var val in l)
    {
        Console.WriteLine(val.Username);
        Console.WriteLine(val.HashedPassword);
        Console.WriteLine(val.TwoFactorAuth);
        Console.WriteLine(val.LastLogin);
    }
}

// await Client.test();

// var ipcaGym = new Gym();

// ipcaGym.Test();

// await Invoice.GenerateInvoicePdf();
