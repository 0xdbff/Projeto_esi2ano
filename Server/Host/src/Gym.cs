using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal class Gym
{
    #region Attributes
    /// <summary>
    ///
    /// </summary>
    internal static List<Client> Clients { get; } = new();

    /// <summary>
    ///
    /// </summary>
    internal static List<Admin> Admins { get; } = new();

    /// <summary>
    ///
    /// </summary>
    internal static List<Trainer> Trainers { get; } = new();

    /// <summary>
    ///
    /// </summary>
    private Guid Id;

    /// <summary>
    ///
    /// </summary>
    public int numClientes { get => Clients.Count(); }

    /// <summary>
    ///
    /// </summary>
    public int numFuncionarios { get => Trainers.Count(); }

    /// <summary>
    ///
    /// </summary>
    internal int lotacaoTotal {get; private set;}

    /// <summary>
    ///
    /// </summary>
    internal int LotacaoAtual {get; private set;}

    /// <summary>
    ///     The gym's mb entity.
    /// </summary>
    public static int MbEntity = 62013;

    private protected Gym() => Id = Guid.NewGuid();

    public void Test() { Console.WriteLine(Id); }

    /// <summary>
    ///     The gym's address.
    /// </summary>
    internal static Address? gymAddress { get; private set; }

    private async Task InsertDefaultGymAsync()
    {
        Id = Guid.NewGuid();

        // var
        //
        // await  CmdExecuteNonQueryAsync(
        //         $"INSERT INTO logindata(username,hashedpassword) VALUES" +
        //         $"('{username}','{passwordHash}')");
    }

    #endregion
}
