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
    private static uint _numClientes;

    /// <summary>
    /// 
    /// </summary>
    private static uint _numFuncionarios;

    /// <summary>
    /// 
    /// </summary>
    private static uint _lotacaoTotal;

    /// <summary>
    /// 
    /// </summary>
    private static uint LotacaoAtual;

    /// <summary>
    /// 
    /// </summary>
    internal Gym()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    internal static string? MbRerefence { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal static string? _address { get; private set; }
    
    #endregion
}