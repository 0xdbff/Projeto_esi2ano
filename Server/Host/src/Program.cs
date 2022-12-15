using Host;
using static Utils.Logger;

LoggerInit();

var client1 = new Client();

await DataBase.DataBase.Test();

Log.Info("10");