namespace Host;

/// <summary>
///
/// </summary>
internal class Subscription
{
    /// <summary> </summary>
    private DateTime nextPaymentDate;

    /// <summary> </summary>
    private DateTime startedDate;

    /// <summary> </summary>
    private bool AutomaticRenewal;

    /// <summary> </summary>
    private string? comments;

    /// <summary> </summary>
    private int nOfCancelations;

    /// <summary> </summary>
    internal StatusEnum Status { get; set; }

    /// <summary> </summary>
    internal TypeEnum Type { get; set; }

    /// <summary>
    ///
    /// </summary>
    internal enum TypeEnum
    {
        Standart,
        Premium,
    }

    /// <summary>
    ///
    /// </summary>
    internal enum StatusEnum
    {
        Active,
        Inactive,
    }

    /// <summary>
    ///
    /// </summary>
    internal Subscription(Subscription.TypeEnum type)
    {
        Type = type;
        Status = StatusEnum.Inactive;
    }

    public Subscription() { }

    int year = DateTime.Now.Year;
    int month = DateTime.Now.Month;

    public async Task<Invoice?>
    GenerateInvoiceForCurrentMonth(Payment.PaymentType type, CreditCard? cc) =>
        await Invoice.GetAsync(type, 2.2, cc,
                               DateOnly.FromDateTime(new DateTime(
                                   DateTime.Now.Year, DateTime.Now.Month, 1)));
}
