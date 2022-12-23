using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

using static Data.DataBase;

namespace Host;

/// <summary>
///     Device's class.
/// </summary>
internal class Device
{
    /// <summary>
    ///     Device's Ip
    /// </summary>
    public IPAddress? iP { get; set; }

    /// <summary>
    ///     Device's LoggedDate
    /// </summary>
    public DateTime LogedDate { get; set; }

    /// <summary>
    ///     Device's Os
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    ///     Device's Hostname
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    ///     Device's Browser
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    ///     Device's Ip location latitude.
    /// </summary>
    public double IpLocationLat { get; set; }

    /// <summary>
    ///     Device's Ip location longitude.
    /// </summary>
    public double IpLocationLon { get; set; }
}
