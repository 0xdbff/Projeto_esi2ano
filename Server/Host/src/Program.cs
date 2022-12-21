using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using Host;

using static Utils.Logger;
using static Utils.Security;

internal class Program
{
    private static async Task Main(string[] args)
    {
        LoggerInit();

        await Client.test();
        // await Invoice.GenerateInvoicePdf();
    }
}
