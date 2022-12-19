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
    private protected Guid Id { get; private set; }

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
    /// The gym's constructor.
    /// </summary>
    internal Gym()
    {
    }

    /// <summary>
    /// The gym's mb reference.
    /// </summary>
    internal static string? MbRerefence { get; private set; }

    /// <summary>
    /// The gym's address.
    /// </summary>
    internal static string? gymAddress { get; private set; }
    
    #endregion
}
