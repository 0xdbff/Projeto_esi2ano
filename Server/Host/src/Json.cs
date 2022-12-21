using System.Text.Json;
using System.Text.Json.Serialization;
using static Data.DataBase;

namespace Host.Json;

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Client))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class ClientJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Admin))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class AdminJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Trainer))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class TrainerJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Device))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class DeviceJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Address))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true, IncludeFields = true)]
public partial class AddressJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Payment))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class PaymentJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Subscription))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class SubscriptionJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Statistics))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class StatisticsJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(CreditCard))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class CreditCardJsonContext : JsonSerializerContext { }

/// <summary>
///
/// </summary>
[JsonSerializable(typeof(Invoice))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
                             WriteIndented = true)]
internal partial class InvoiceJsonContext : JsonSerializerContext { }
