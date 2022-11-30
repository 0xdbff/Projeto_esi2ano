using host;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.Threading;
using static host.Utils;

LoggerInit();

var Client = new Client();

//await DataBase.Test();