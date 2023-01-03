namespace Host;

using Data;
using System.Text.Json;

using static Data.DataBase;
using static Utils.Logger;
using static Utils.File;
using Host.Json;

/// <summary>
///     Address Class, Identifies a location.
/// </summary>
public sealed class Address
{
    /// <summary>
    ///     A unique code that identifies an address.
    /// </summary>
    //! TODO cannot insert to database
    public Guid? Code { get; private set; }

    /// <summary>
    ///     The client's address -> Postal.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    ///     The client's address -> Country.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    ///     The client's address -> City.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    ///     The client's address -> Last time updated.
    /// </summary>
    public DateTime? LastUpdate { get; set; }

    /// <summary>
    ///     The client's address -> Additional info.
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    ///     The client's address -> House number.
    /// </summary>
    public int HouseNum { get; set; }

    /// <summary>
    ///     The client's address -> Localidade.
    /// </summary>
    public string? Localidade { get; set; }

    /// <summary>
    ///     Await some service to verify given address (class instance);
    ///     !TODO update service, always evaluates to <see langword="true"/>.
    /// </summary>
    /// <returns> An awaitable Task with address validation. </returns>
    public async Task<bool>
    IsAddressValidAsync() => await Task<bool>.Run(() => true);

    /// <summary>
    ///     Serialize Address.
    /// </summary>
    /// <param name="address">address instance </param>
    /// <returns> a json string </returns>
    public static string? FromClassToJson(Address address) =>
        JsonSerializer.Serialize(address, AddressJsonContext.Default.Address);

    /// <summary>
    ///     An address example.
    /// </summary>
    /// <returns>An instance of address. </returns>
    public static Address GenExample1() => new Address("4720-000", "Portugal",
                                                       "Braga", DateTime.Now,
                                                       "Rua exemplo", 20,
                                                       "Amares");

    /// <summary>
    ///     Address default constructor
    /// </summary>
    public Address() { }

    /// <summary>
    ///     Construct an address from json.
    ///     Deserialize
    /// </summary>
    /// <param name="json"> json </param>
    /// <returns> An instance of Address </returns>
    public static Address? FromJson(string json) =>
        JsonSerializer.Deserialize(json, AddressJsonContext.Default.Address);

    /// <summary>
    ///     Construct an instance of Address with all values.
    /// </summary>
    /// <param name="postalCode"> Postal Code </param>
    /// <param name="country"> Country </param>
    /// <param name="city"> City </param>
    /// <param name="lastUpdate"> Last Time updated </param>
    /// <param name="aditionalInfo"> additional info </param>
    /// <param name="houseNum"> house number </param>
    /// <param name="localidade"> localidade </param>
    public Address(string postalCode, string country, string city,
                   DateTime lastUpdate, string? aditionalInfo, int houseNum,
                   string localidade)
    {
        PostalCode = postalCode;
        Country = country;
        City = city;
        LastUpdate = lastUpdate;
        AdditionalInfo = aditionalInfo;
        HouseNum = houseNum;
        Localidade = localidade;
    }

    /// <summary>
    ///     To string method for Address type
    /// </summary>
    /// <returns> a string with address.</returns>
    public override
        string? ToString() => $"{Localidade}-{City}, {Country}\n" +
                              $"{PostalCode}, {AdditionalInfo} n{HouseNum}";

    /// <summary>
    ///     Convert to string to be used in invoice latex compilation.
    /// </summary>
    /// <returns> an encoded string </returns>
    public string? ToStringLatex() =>
        $"{Localidade}-{City}, {Country}\\\\" + "\n" +
        $"{PostalCode}, {AdditionalInfo} n{HouseNum} \\\\";

    //! TODO remove static
    /// <summary>
    ///     Insert address to Database.
    /// </summary>
    /// <param name="a">Address</param>
    /// <param name="username">username</param>
    internal static async Task InsertToDbAsync(Address a, string username)
    {
        try
        {
            await CmdExecuteQueryAsync(
                $"INSERT into address (code, postalcode, country, city,  lastupdatedate, " +
                $"aditionalinfo, housenum, localidade, username) VALUES ( (SELECT (gen_random_uuid ())) " +
                $", '{a.PostalCode}', '{a.Country}', '{a.City}', (SELECT NOW()) , '{a.AdditionalInfo}' " +
                $", 2, '{a.Localidade}' , '{username}' )");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    ///     Get and instance of address with a username.
    /// </summary>
    /// <param name="username">username</param>
    /// <returns>An instance of address related to the user</returns>
    internal static async Task<Address?> GetWithUsernameAsync(string username)
    {
        try
        {
            var values = await CmdExecuteQuerySingleAsync(
                $"SELECT * from address WHERE username = '{username}' LIMIT 1;");

            var data = new Address();

            foreach (var val in from column in values
                                where values is not null
                                where column.Value is not System.DBNull
                                select column)
                switch (val.Key)
                {
                    // case 0:
                    //     data.Code = (Guid)val.Value;
                    //     break;
                    case 1:
                        data.PostalCode = (String)val.Value;
                        break;
                    case 2:
                        data.Country = (String)val.Value;
                        break;
                    case 3:
                        data.City = (String)val.Value;
                        break;
                    case 4:
                        data.LastUpdate = (DateTime)val.Value;
                        break;
                    case 5:
                        data.AdditionalInfo = (String)val.Value;
                        break;
                    case 6:
                        data.HouseNum = (int)val.Value;
                        break;
                    case 7:
                        data.Localidade = (string)val.Value;
                        break;
                }

            return data;
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///     Equals override
    /// </summary>
    /// <param name="obj"> An instance of an object</param>
    /// <returns> a boolean </returns>
    public override bool Equals(object? obj) => obj is Address
                                                    ? Code == ((Address)obj).Code
                                                    : false;

    /// <summary>
    ///     Get hash code override
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Code.GetHashCode();
}
