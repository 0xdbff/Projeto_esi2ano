namespace Host;

/// <summary>
///
/// </summary>
internal sealed class Invoice : Payment
{
    /// <summary>
    ///
    /// </summary>
    private bool includeNif = true;

    /// <summary>
    ///
    /// </summary>
    public DateOnly Month { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    private Invoice(PaymentType type, double amount, CreditCard? cc)
        : base(type, amount, cc) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    public static async Task<Invoice?> GetAsync(PaymentType type, double amount,
                                             CreditCard? cc, DateOnly month)
    {
        var invoice = new Invoice(type, amount, cc);
        invoice.Month = month;

        await invoice.PaymentAsync(type, amount, cc);

        if (invoice.Status == PaymentStatus.Expired)
            return null;

        return invoice;
    }

    /// <summary>
    ///
    /// </summary>
    private void GenerateInvoicePdf() { }
}
