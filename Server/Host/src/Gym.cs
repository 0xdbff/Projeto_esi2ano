using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal class Gym
{
    /// <summary>
    ///
    /// </summary>
    internal static List<Client>? Clients { get; }

    /// <summary>
    ///
    /// </summary>
    internal static List<Admin>? Admins { get; }

    /// <summary>
    ///
    /// </summary>
    internal static List<Trainer>? Trainers { get; }

    #region Attributes

    /// <summary>
    ///
    /// </summary>
    private Guid Id;

    /// <summary>
    ///
    /// </summary>
    public static uint numClientes;

    /// <summary>
    ///
    /// </summary>
    private static uint numFuncionarios;

    /// <summary>
    ///
    /// </summary>
    private static uint lotacaoTotal;

    /// <summary>
    ///
    /// </summary>
    private static uint LotacaoAtual;

    /// <summary>
    ///     The gym's mb reference.
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
        //
        Id = Guid.NewGuid();

        // var 
        //
        // await  CmdExecuteNonQueryAsync(
        //         $"INSERT INTO logindata(username,hashedpassword) VALUES" +
        //         $"('{username}','{passwordHash}')");
    }

    #endregion
}
