using Data;
using static Utils.Logger;
using static Data.DataBase;

namespace Host;

/// <summary>
///     Payment status used.
/// </summary>
internal enum PaymentStatus
{
    /// <summary>
    ///     Waiting for payment.
    /// </summary>
    WaitingPayment,
    /// <summary>
    ///     Expired.
    /// </summary>
    Expired,
    /// <summary>
    ///     Completed.
    /// </summary>
    Completed,
}

/// <summary>
///
/// </summary>
internal enum PaymentType
{
    /// <summary> MB reference </summary>
    MbRef,
    /// <summary> Credit Card </summary>
    CreditCard,
}

/// <summary>
///     Mb reference Class
/// </summary>
internal class MbRef
{
    /// <summary>
    ///     reference expiry date.
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    ///     Mb reference.
    /// </summary>
    public int Value { get; private set; }

    /// <summary>
    ///     Mb ref constructor.
    /// </summary>
    public MbRef()
    {
        ExpiryDate = (DateTime.Now).AddDays(1);
        Value = GenerateMbReference();
    }

    /// <summary>
    ///     Generate an instance of Mb reference
    /// </summary>
    private static int GenerateMbReference() =>
        // !TODO communicate with related services to get mb reference.
        (new Random()).Next(0, 999999999);
}

/// <summary>
///     Payment class.
/// </summary>
internal class Payment
{
    /// <summary>
    ///     payment's expiry Date.
    /// </summary>
    public DateTime? ExpiryDate { get; private set; }

    /// <summary>
    ///     payment's paid data.
    /// </summary>
    public DateTime? PaidDate { get; private set; }

    /// <summary>
    ///     payment amount.
    /// </summary>
    public double Amount { get; private set; }

    /// <summary>
    ///     payment status.
    /// </summary>
    public PaymentStatus Status { get; private set; }

    /// <summary>
    ///     payment type used.
    /// </summary>
    public PaymentType PaymentTypeUsed { get; private set; }

    /// <summary>
    ///     Mb reference instance.
    /// </summary>
    public MbRef? MbReference { get; private set; }

    #region Attributes

    #endregion

    /// <summary>
    ///     Payment constructor
    /// </summary>
    /// <param name="type"> type </param>
    /// <param name="amount"> amount </param>
    /// <param name="cc"> credit card </param>
    private protected Payment(PaymentType type, double amount, CreditCard? cc)
    {
        Amount = amount;
        PaymentTypeUsed = type;
        PaidDate = null;
        ExpiryDate = DateTime.Now.AddDays(8);
        Status = PaymentStatus.WaitingPayment;
    }

    /// <summary>
    ///     Try a payment asynchronously.
    /// </summary>
    /// <param name="type">type </param>
    /// <param name="amount">amount </param>
    /// <param name="cc">credit card </param>
    /// <returns>An instance of a payment </returns>
    public async Task PaymentAsync(PaymentType type, double amount,
                                   CreditCard? cc)
    {
        if (type == PaymentType.MbRef)
        {
            MbReference = new();
            Status = await TryPaymentRefMbAsync(MbReference);
        }
        else
        {
            if (cc != null)
                Status = await TryPaymentCcAsync(cc);
        }
    }

    /// <summary>
    ///     SIMULATED CODE, to verify payment was completed.
    /// </summary>
    /// <returns>Banking validation</returns>
    private static async Task<bool> SomeBankingServiceAsync() =>
        // SIMULATED CODE
        await Task<bool>.Run(() => true);

    /// <summary>
    ///     Try a payment with a credit card
    /// </summary>
    /// <param name="cc"></param>
    /// <returns> payment status </returns>
    private async Task<PaymentStatus> TryPaymentCcAsync(CreditCard cc)
    {
        // !TODO this code needs to be changed... This line of code is also
        // pointless!, (Payment always evaluates to completed).
        if (ExpiryDate < DateTime.Now)
            return PaymentStatus.Expired;

        // !TODO Connect to bank services.
        // And await payment with a background thread.
        bool paid = await SomeBankingServiceAsync();

        if (paid)
        {
            PaidDate = DateTime.Now;
            return PaymentStatus.Completed;
        }
        return PaymentStatus.WaitingPayment;
    }

    /// <summary>
    ///     try a payment with mb reference.
    /// </summary>
    /// <param name="mbReference"></param>
    /// <returns>payment status </returns>
    private async Task<PaymentStatus> TryPaymentRefMbAsync(MbRef mbReference)
    {
        // !TODO this code needs to be changed... This line of code is also
        // pointless!, (Payment always evaluates to completed).
        if (ExpiryDate < DateTime.Now)
            return PaymentStatus.Expired;

        // !TODO Connect to bank services.
        // And await payment with a background thread.
        bool paid = await SomeBankingServiceAsync();

        if (paid)
        {
            PaidDate = DateTime.Now;
            return PaymentStatus.Completed;
        }
        return PaymentStatus.WaitingPayment;
    }
}