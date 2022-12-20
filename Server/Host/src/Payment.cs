using Data;
using static Utils.Logger;
using static Data.DataBase;

namespace Host;

internal enum CcType
{
    /// <summary> </summary>
    Invalid,
    /// <summary> </summary>
    Visa,
    /// <summary> </summary>
    MasterCard,
}

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
internal class Payment
{

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
        public DateTime? ExpiryDate { get; set; }

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
        private int GenerateMbReference() =>
            // !TODO communicate with related services to get mb reference.
            (new Random()).Next(0, 999999999);
    }

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
    private protected Payment(PaymentType type, double amount, CreditCard? cc)
    {
        Amount = amount;
        PaymentTypeUsed = type;
        PaidDate = null;
        ExpiryDate = DateTime.Now.AddDays(8);
        Status = PaymentStatus.WaitingPayment;
    }

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

    // private async Task<bool> SomeBankingServiceAsync()
    // {
    //     var fn = () => true;
    //     new Task(fn);
    //     //
    // }

    /// <summary>
    ///
    /// </summary>
    private async Task<PaymentStatus> TryPaymentCcAsync(CreditCard cc)
    {
        // !TODO this code needs to be changed...
        if (ExpiryDate < DateTime.Now)
            return PaymentStatus.Expired;

        // !TODO Connect to bank services.
        // And await payment with a background thread for the active period.
        bool paid = true;

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
    private async Task<PaymentStatus> TryPaymentRefMbAsync(MbRef mbReference)
    {
        // !TODO this code needs to be changed...
        if (ExpiryDate < DateTime.Now)
            return PaymentStatus.Expired;

        // !TODO Connect to bank services.
        // And await payment with a background thread for the active period.
        bool paid = true;

        if (paid)
        {
            PaidDate = DateTime.Now;
            return PaymentStatus.Completed;
        }
        return PaymentStatus.WaitingPayment;
    }
}
