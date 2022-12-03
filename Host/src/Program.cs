using Host;
using static Host.Utils;

LoggerInit();

var client1 = new Client();

//await DataBase.Test();

client1.UpdateHeight(1.1);

client1.UpdateWeight(59.999999);

Console.WriteLine(client1.CurrentImc);

Console.WriteLine(client1.ImcValue);