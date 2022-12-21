namespace Host;

using System.Text.Json;
using System.Text.Json.Serialization;

using Host.Json;

/// <summary>
///
/// </summary>
internal class Address
{
    /// <summary>
    ///
    /// </summary>
    public int PostalCode { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public string? Country { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public string? City { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? LastUpdate { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public string? AditionalInfo { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public int HouseNum { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public string? Localidade { get; private set; }

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
    public Address(int postalCode, string country, string city,
                   DateTime lastUpdate, string? aditionalInfo, int houseNum,
                   string localidade)
    {
        PostalCode = postalCode;
        Country = country;
        City = city;
        LastUpdate = lastUpdate;
        AditionalInfo = aditionalInfo;
        HouseNum = houseNum;
        Localidade = localidade;
    }
}