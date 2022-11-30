namespace Host;

/// <summary>
/// 
/// </summary>
internal class Invoice : Payment
{
    /// <summary>
    /// 
    /// </summary>
    private bool _includeNif = true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeNif"></param>
    public Invoice(bool includeNif)
    {
        _includeNif = includeNif;
    }

    /// <summary>
    /// 
    /// </summary>
    public Invoice()
    {
    }
}
