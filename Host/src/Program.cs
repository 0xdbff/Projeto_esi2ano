using Host;
using static Host.Utils;

LoggerInit();

var client1 = new Client();

Console.WriteLine(await DataBase.Test());
