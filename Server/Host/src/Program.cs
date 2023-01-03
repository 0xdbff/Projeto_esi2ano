using Host;

using static Utils.Logger;

LoggerInit();

await Data.DataBase.Init();

await Admin.Init();

var admin = await Admin.GetWithUsernameAsync("IpcaGymAdmin");

if (admin != null)
    Console.WriteLine(admin.Address.ToString());

await GenRandom.Init50usernames();

var allClients = await Client.GetAll();

if (allClients != null)
    foreach (var client in allClients)
    {
        if (client != null)
        {
            await GenRandom.ClientReqwestsSubscriptionSimulatedAsync(client);
        }
    }
