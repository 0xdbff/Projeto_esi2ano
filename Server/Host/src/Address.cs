namespace Host;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Address))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             // PropertyNamingPolicy =
                             //     JsonKnownNamingPolicy.CamelCase,
                             WriteIndented = true)]
internal partial class AddressJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
internal class Address
{
    /// <summary>
    ///
    /// </summary>
    public int PostalCode { get; set; }
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
    public string? AditionalInfo { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int HouseNum { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Localidade { get; set; }

    public static string? AddressFromClassToJson(Address address)
    {
        return JsonSerializer.Serialize(address,
                                        AddressJsonContext.Default.Address);
    }

    public Address() { }

    public static Address? FromJson(string json) =>
        JsonSerializer.Deserialize(json, AddressJsonContext.Default.Address);

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
