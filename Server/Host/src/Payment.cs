namespace Host;

using static Data.DataBase;

internal enum CcType
{
    /// <summary> </summary>
    Visa,
    /// <summary> </summary>
    MasterCard,
}

internal class CreditCard
{
    internal UInt64 CcNum { get; }

    internal DateTime ExpiryDate { get; }

    internal DateTime InsertedDate { get; }

    internal string? SecurityCode { get; }

    internal string? CcName { get; }

    internal CcType CreditCardType { get; }

    internal CreditCard(UInt64 ccNum, DateTime expiryDate, DateTime insertedDate,
                        string? securityCode, string? ccName,
                        CcType creditCardType)
    {
        CcNum = ccNum;
        ExpiryDate = expiryDate;
        InsertedDate = insertedDate;
        SecurityCode = securityCode;
        CcName = ccName;
        CreditCardType = creditCardType;
    }

    internal static async Task<int>
    InsertCreditCard(Guid clientId, CreditCard cc) => await CmdExecuteNonQueryAsync(
        $"INSERT into creditcard (CcNum, ClientId, ExpiryDate, SecurityCode, NameInCC)" +
        $"WITH VALUES ({cc.CcNum}, {clientId}, {cc.ExpiryDate},{cc.InsertedDate},{cc.CcName}," +
        $"{cc.CreditCardType})");
}

/// <summary>
///
/// </summary>
internal class Payment : Client
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
