using static Data.DataBase;

namespace Host;

/// <summary>
///     Gym's Class
/// </summary>
internal class Gym
{
    #region Attributes

    // internal List<Client>? Clients { get; set; }
    //
    // /// <summary>
    // ///
    // /// </summary>
    // internal List<Admin>? Admins { get; set; }
    //
    // /// <summary>
    // ///
    // /// </summary>
    // internal List<Trainer>? Trainers { get; set; }

    /// <summary>
    ///
    /// </summary>
    private Guid _id;

    // /// <summary>
    // ///
    // /// </summary>
    // public int numClientes { get => Clients != null ? Clients.Count() : 0; }
    //
    // /// <summary>
    // ///
    // /// </summary>
    // public int numFuncionarios { get => Trainers != null ? Trainers.Count() : 0; }

    /// <summary>
    ///     Lotation of the Gym
    /// </summary>
    internal int lotacaoTotal { get; private set; }

    /// <summary>
    ///     Current number of clients in the gym.
    /// </summary>
    internal int LotacaoAtual { get; private set; }

    /// <summary>
    ///     The gym's mb entity.
    /// </summary>
    public static int MbEntity = 62013;

    /// <summary>
    ///     Gym's constructor.
    /// </summary>
    private protected Gym() { }

    /// <summary>
    ///     The gym's address.
    /// </summary>
    internal static Address? gymAddress { get; private set; }

    /// <summary>
    ///     Insert default gym
    /// </summary>
    private async Task InsertDefaultGymAsync()
    {
        _id = Guid.NewGuid();

        // await  CmdExecuteNonQueryAsync(
        //         $"INSERT INTO logindata(username,hashedpassword) VALUES" +
        //         $"('{username}','{passwordHash}')");
    }

    #endregion
}
