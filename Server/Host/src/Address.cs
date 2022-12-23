namespace Host;

using System.Text.Json;
using static Data.DataBase;
using static Utils.Logger;
using Data;

using Host.Json;

/// <summary>
///
/// </summary>
public sealed class Address
{
    /// <summary>
    ///
    /// </summary>
    public Guid? Code { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? LastUpdate { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int HouseNum { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Localidade { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Task<bool> IsAddressValidAsync { get => ValidateAddress(); }

    /// <summary>
    ///     Await some service to verify given address;
    /// </summary>
    /// <returns> An awaitable Task with address validation. </returns>
    private async Task<bool>
    ValidateAddress() => await Task<bool>.Run(() => true);

    /// <summary>
    /// /
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string? FromClassToJson(Address address)
    {
        return JsonSerializer.Serialize(address,
                                        AddressJsonContext.Default.Address);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static Address GenExample1() => new Address("4720-000", "Portugal",
                                                       "Braga", DateTime.Now,
                                                       "Rua exemplo", 20,
                                                       "Amares");

    /// <summary>
    ///
    /// </summary>
    public Address() { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static Address? FromJson(string json) =>
        JsonSerializer.Deserialize(json, AddressJsonContext.Default.Address);

    /// <summary>
    ///
    /// </summary>
    /// <param name="postalCode"></param>
    /// <param name="country"></param>
    /// <param name="city"></param>
    /// <param name="lastUpdate"></param>
    /// <param name="aditionalInfo"></param>
    /// <param name="houseNum"></param>
    /// <param name="localidade"></param>
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
    ///
    /// </summary>
    /// <returns></returns>
    public override
        string? ToString() => $"{Localidade}-{City}, {Country}\n" +
                              $"{PostalCode}, {AdditionalInfo} n{HouseNum}";

    public override bool Equals(object? obj) => obj is Address
                                                    ? Code == ((Address)obj).Code
                                                    : false;

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public string? ToStringLatex() =>
        $"{Localidade}-{City}, {Country}\\\\" + "\n" +
        $"{PostalCode}, {AdditionalInfo} n{HouseNum} \\\\";

    /// <summary>
    ///
    /// </summary>
    internal async Task InsertToDb(string username)
    {
        try
        {
            await CmdExecuteNonQueryAsync(
                $"INSERT into address(code, postalcode, country, city,  lastupdatedate," +
                $"aditionalinfo, housenum, localidade, username) VALUES" +
                $"({Code},'{PostalCode}','{Country}','{City}',(SELECT NOW())," +
                $"'{AdditionalInfo}',{HouseNum},'{Localidade}','{username}');");
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
        }
    }

    internal static async Task<Address?> GetWithUsername(string username)
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
                    case 0:
                        data.Code = (Guid)val.Value;
                        break;
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

    public override int GetHashCode() => Code.GetHashCode();
}
