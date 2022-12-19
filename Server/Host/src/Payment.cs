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
internal class Payment
{

    /// <summary>
    ///
    /// </summary>
    internal enum Type
    {
        /// <summary> MB reference </summary>
        MbRef,
        /// <summary> Credit Card </summary>
        CreditCard,
    }

    /// <summary>
    ///
    /// </summary>
    internal enum Status
    {
        /// <summary>
        ///
        /// </summary>
        Completed,
        /// <summary>
        ///
        /// </summary>
        Expired,
    }

    /// <summary>
    ///
    /// </summary>
    private protected DateTime? ExpiryDate { get; private set; }

    /// <summary>
    ///
    /// </summary>
    private protected DateTime? PaidDate { get; private set; }

    /// <summary>
    ///
    /// </summary>
    private protected double Amount { get; private set; }

    /// <summary>
    ///
    /// </summary>
    private protected string? Info { get; private set; }

    #region Attributes

    #endregion

    /// <summary>
    /// Await the client's payment;
    /// </summary>
    /// <returns></returns>
    internal async Task<Status> Await() { return Status.Completed; }

    /// <summary>
    /// Notify the client to pay the required amount in a given period of time.
    /// </summary>
    /// <returns></returns>
    internal void Notify() { }
}
