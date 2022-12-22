namespace Host;

/// <summary>
///
/// </summary>
internal enum SubscriptionStatus
{
    /// <summary>
    ///
    /// </summary>
    Active,
    /// <summary>
    ///
    /// </summary>
    Inactive,
}

/// <summary>
///
/// </summary>
internal enum SubscriptionPlan
{
    /// <summary>
    ///
    /// </summary>
    Standart,
    /// <summary>
    ///
    /// </summary>
    Premium,
}

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
    internal SubscriptionStatus Status { get; set; }

    /// <summary> </summary>
    internal SubscriptionPlan Type { get; set; }

    /// <summary>
    ///
    /// </summary>
    internal Subscription(SubscriptionPlan type)
    {
        Type = type;
        Status = SubscriptionStatus.Inactive;
    }

    /// <summary>
    ///
    /// </summary>
    public Subscription() { }

    private static async Task checkPayments()
    {
        Thread thread = new Thread(() =>
        {
            // check every 24h.
            const int MillisecondResponseRate = 10000;
            do
            {
                Thread.Sleep(MillisecondResponseRate);
                // CheckTasks();
            } while (true);
        });
        thread.Start();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="type"></param>
    /// <param name="cc"></param>
    /// <returns></returns>
    public async Task<Invoice?> GenerateInvoiceForCurrentMonth(PaymentType type,
                                                               CreditCard? cc) =>
        await Invoice.GetAsync(type, 2.2, cc,
                               DateOnly.FromDateTime(new DateTime(
                                   DateTime.Now.Year, DateTime.Now.Month, 1)));
}
