namespace Host;

/// <summary>
///     Subscription Status
/// </summary>
internal enum SubscriptionStatus
{
    /// <summary>
    ///     Active
    /// </summary>
    Active,
    /// <summary>
    ///     Inactive
    /// </summary>
    Inactive,
}

/// <summary>
///     Subscription Plan
/// </summary>
internal enum SubscriptionPlan
{
    /// <summary>
    ///     Standard
    /// </summary>
    Standart,
    /// <summary>
    ///     Premium
    /// </summary>
    Premium,
}

/// <summary>
///     Subscription Class
/// </summary>
internal class Subscription
{
    /// <summary> next payment date</summary>
    private DateTime nextPaymentDate;

    /// <summary> started date </summary>
    private DateTime startedDate;

    /// <summary> automatic renewal </summary>
    private bool AutomaticRenewal;

    /// <summary> comments </summary>
    private string? comments;

    /// <summary> number of cancellations </summary>
    private int nOfCancelations;

    /// <summary>  Subscription Status </summary>
    internal SubscriptionStatus Status { get; set; }

    /// <summary> Subscription Type </summary>
    internal SubscriptionPlan Type { get; set; }
    
    /// <summary>
    ///     Subscription constructor 
    /// </summary>
    /// <param name="type"> type </param>
    internal Subscription(SubscriptionPlan type)
    {
        Type = type;
        Status = SubscriptionStatus.Inactive;
    }

    /// <summary>
    ///     Subscription constructor.
    /// </summary>
    public Subscription() { }

    /// <summary>
    ///     Check payment status.
    /// </summary>
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
    ///     Get invoice async for current month.
    /// </summary>
    /// <param name="type"> type </param>
    /// <param name="cc">credit card </param>
    /// <returns></returns>
    public async Task<Invoice?> GenerateInvoiceForCurrentMonth(PaymentType type,
                                                               CreditCard? cc) =>
        await Invoice.GetAsync(type, 2.2, cc,
                               DateOnly.FromDateTime(new DateTime(
                                   DateTime.Now.Year, DateTime.Now.Month, 1)));
}
