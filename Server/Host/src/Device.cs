using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal class Device
{
    /// <summary>
    ///
    /// </summary>
    public IPAddress? iP { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime LogedDate { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    ///
    /// </summary>
    public double IpLocationLat { get; set; }

    /// <summary>
    ///
    /// </summary>
    public double IpLocationLon { get; set; }
}
