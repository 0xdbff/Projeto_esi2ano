using Host;
using static Utils.Security;
using static Utils.Logger;
using static Utils.File;

internal static class GenRandom
{

    public static string FilesPath = "/home/db/dev/repo_g06/Data/random/";

    public static async Task InitAllUsernames()
    {
        try
        {
            string[] usernames =
                await File.ReadAllLinesAsync(FilesPath + "usernames.txt");
            foreach (var user in Enumerable.Range(1, usernames.Length))
            {
                string[] firstnames =
                    await File.ReadAllLinesAsync(FilesPath + "firstNames.txt");
                string[] lastnames =
                    await File.ReadAllLinesAsync(FilesPath + "lastNames.txt");
                string[] passwords =
                    await File.ReadAllLinesAsync(FilesPath + "passwords.txt");
                string[] cities =
                    await File.ReadAllLinesAsync(FilesPath + "cyties.txt");
                string[] localities =
                    await File.ReadAllLinesAsync(FilesPath + "localities.txt");

                string username = usernames[user - 1];
                string firstname = firstnames[new Random().Next(firstnames.Length)];
                string lastname = lastnames[new Random().Next(lastnames.Length)];
                string city = cities[new Random().Next(cities.Length)];
                string locality = localities[new Random().Next(localities.Length)];
                string password = passwords[new Random().Next(passwords.Length)];

                var hash = SHA512(password);
                var postalCode =
                    ($"{new Random().Next(4000, 5000)}-{new Random().Next(100, 999)}");
                var houseNum = new Random().Next(0, 999);

                var oneGender = new Random().Next(0, 3) == 1;
                Gender gender;
                if (oneGender)
                    gender = Gender.Male;
                else
                    gender = Gender.Female;

                var dateOfBirth =
                    new DateTime(new Random().Next(1990, 2019),
                                 new Random().Next(1, 12), new Random().Next(1, 12));
                var NotAcademic = new Random().Next(1, 5) == 1;
                ClientType clientType = ClientType.Invalid;
                string email = " ";
                if (NotAcademic)
                {
                    email = $"{username}@gmail.example";
                    clientType = ClientType.Common;
                }
                else
                {
                    clientType = ClientType.Academic;
                    email = $"alunos.{username}@ipca.example.pt";
                }
                var nif = new Random().Next(100000000, 999999999);
                var addr = new Address(postalCode, "Portugal Continental", city,
                                       DateTime.Now, "Rua Exemplo", houseNum, locality);
                var client = await Client.NewClientAsync(
                    firstname, lastname, gender, dateOfBirth, nif, addr, email,
                    clientType, username, hash, "not set");
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    public static async Task Init1000usernames()
    {
        try
        {
            string[] usernames =
                await File.ReadAllLinesAsync(FilesPath + "usernames.txt");
            foreach (var user in Enumerable.Range(1, 1000))
            {
                string[] firstnames =
                    await File.ReadAllLinesAsync(FilesPath + "firstNames.txt");
                string[] lastnames =
                    await File.ReadAllLinesAsync(FilesPath + "lastNames.txt");
                string[] passwords =
                    await File.ReadAllLinesAsync(FilesPath + "passwords.txt");
                string[] cities =
                    await File.ReadAllLinesAsync(FilesPath + "cyties.txt");
                string[] localities =
                    await File.ReadAllLinesAsync(FilesPath + "localities.txt");

                string username = usernames[user - 1];
                string firstname = firstnames[new Random().Next(firstnames.Length)];
                string lastname = lastnames[new Random().Next(lastnames.Length)];
                string city = cities[new Random().Next(cities.Length)];
                string locality = localities[new Random().Next(localities.Length)];
                string password = passwords[new Random().Next(passwords.Length)];

                var hash = SHA512(password);
                var postalCode =
                    ($"{new Random().Next(4000, 5000)}-{new Random().Next(100, 999)}");
                var houseNum = new Random().Next(0, 999);

                var oneGender = new Random().Next(0, 3) == 1;
                Gender gender;
                if (oneGender)
                    gender = Gender.Male;
                else
                    gender = Gender.Female;

                var dateOfBirth =
                    new DateTime(new Random().Next(1990, 2019),
                                 new Random().Next(1, 12), new Random().Next(1, 12));
                var NotAcademic = new Random().Next(1, 5) == 1;
                ClientType clientType = ClientType.Invalid;
                string email = " ";
                if (NotAcademic)
                {
                    email = $"{username}@gmail.example";
                    clientType = ClientType.Common;
                }
                else
                {
                    clientType = ClientType.Academic;
                    email = $"alunos.{username}@ipca.example.pt";
                }
                var nif = new Random().Next(100000000, 999999999);
                var addr = new Address(postalCode, "Portugal Continental", city,
                                       DateTime.Now, "Rua Exemplo", houseNum, locality);
                var client = await Client.NewClientAsync(
                    firstname, lastname, gender, dateOfBirth, nif, addr, email,
                    clientType, username, hash, "not set");
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    public static async Task Init50usernames()
    {
        try
        {
            string[] usernames =
                await File.ReadAllLinesAsync(FilesPath + "usernames.txt");
            foreach (var user in Enumerable.Range(1, 50))
            {
                string[] firstnames =
                    await File.ReadAllLinesAsync(FilesPath + "firstNames.txt");
                string[] lastnames =
                    await File.ReadAllLinesAsync(FilesPath + "lastNames.txt");
                string[] passwords =
                    await File.ReadAllLinesAsync(FilesPath + "passwords.txt");
                string[] cities =
                    await File.ReadAllLinesAsync(FilesPath + "cyties.txt");
                string[] localities =
                    await File.ReadAllLinesAsync(FilesPath + "localities.txt");

                string username = usernames[user - 1];
                string firstname = firstnames[new Random().Next(firstnames.Length)];
                string lastname = lastnames[new Random().Next(lastnames.Length)];
                string city = cities[new Random().Next(cities.Length)];
                string locality = localities[new Random().Next(localities.Length)];
                string password = passwords[new Random().Next(passwords.Length)];

                var hash = SHA512(password);
                var postalCode =
                    ($"{new Random().Next(4000, 5000)}-{new Random().Next(100, 999)}");
                var houseNum = new Random().Next(0, 999);

                var oneGender = new Random().Next(0, 3) == 1;
                Gender gender;
                if (oneGender)
                    gender = Gender.Male;
                else
                    gender = Gender.Female;

                var dateOfBirth =
                    new DateTime(new Random().Next(1990, 2019),
                                 new Random().Next(1, 12), new Random().Next(1, 12));
                var NotAcademic = new Random().Next(1, 5) == 1;
                ClientType clientType = ClientType.Invalid;
                string email = " ";
                if (NotAcademic)
                {
                    email = $"{username}@gmail.example";
                    clientType = ClientType.Common;
                }
                else
                {
                    clientType = ClientType.Academic;
                    email = $"alunos.{username}@ipca.example.pt";
                }
                var nif = new Random().Next(100000000, 999999999);
                var addr = new Address(postalCode, "Portugal Continental", city,
                                       DateTime.Now, "Rua Exemplo", houseNum, locality);
                var client = await Client.NewClientAsync(
                    firstname, lastname, gender, dateOfBirth, nif, addr, email,
                    clientType, username, hash, "not set");
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    internal static async
        Task ClientReqwestsSubscriptionSimulatedAsync(Client client)
    {
        var type = new Random().Next(0, 3) == 1;
        SubscriptionPlan sp;

        if (type)
            sp = SubscriptionPlan.Premium;
        else
            sp = SubscriptionPlan.Standart;

        // gen subscription of random type
        var sub = new Subscription(sp);

        client.Subscrive(sub);

        await sub.GenerateInvoiceForCurrentMonth(client, PaymentType.CreditCard,
                                                 null);
    }
}
