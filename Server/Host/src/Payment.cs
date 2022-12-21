using Data;
using static Utils.Logger;
using static Data.DataBase;

namespace Host;

/// <summary>
///
/// </summary>
internal enum PaymentStatus
{
    /// <summary>
    ///
    /// </summary>
    WaitingPayment,
    /// <summary>
    ///
    /// </summary>
    Expired,
    /// <summary>
    ///
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
///
/// </summary>
internal class MbRef
{
    /// <summary>
    ///
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int Value { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public MbRef()
    {
        ExpiryDate = (DateTime.Now).AddDays(1);
        Value = GenerateMbReference();
    }

    /// <summary>
    ///
    /// </summary>
    private static int GenerateMbReference() =>
        // !TODO communicate with related services to get mb reference.
        (new Random()).Next(0, 999999999);
}

/// <summary>
///
/// </summary>
internal class Payment
{
    /// <summary>
    ///
    /// </summary>
    public DateTime? ExpiryDate { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? PaidDate { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public double Amount { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public PaymentStatus Status { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public PaymentType PaymentTypeUsed { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public MbRef? MbReference { get; private set; }

    #region Attributes

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    private protected Payment(PaymentType type, double amount, CreditCard? cc)
    {
        Amount = amount;
        PaymentTypeUsed = type;
        PaidDate = null;
        ExpiryDate = DateTime.Now.AddDays(8);
        Status = PaymentStatus.WaitingPayment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cc"></param>
    /// <returns></returns>
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
    /// <returns></returns>
    private static async Task<bool> SomeBankingServiceAsync() =>
        // SIMULATED CODE
        await Task<bool>.Run(() => true);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cc"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="mbReference"></param>
    /// <returns></returns>
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
