using Host;
using static Host.Utils;

LoggerInit();

try
{

    var client1 = new Client();

    //await DataBase.Test();

    client1.UpdateHeight(2.39);

    client1.UpdateWeight(20.1);

    Console.WriteLine(client1.CurrentBmi);

    Console.WriteLine(client1.BmiValue);
}
catch
{
    
}