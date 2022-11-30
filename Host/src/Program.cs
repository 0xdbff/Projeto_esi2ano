using Host;
using static Host.Utils;

LoggerInit();

var Client = new Client();

await DataBase.Test();