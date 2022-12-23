using Data;
using static Utils.Logger;
using static Data.DataBase;

namespace Host;

/// <summary>
///     Credit Card Type.
/// </summary>
internal enum CcType
{
    /// <summary> Invalid Credit Card. </summary>
    Invalid,
    /// <summary> Visa credit Card.  </summary>
    Visa,
    /// <summary> Mastercard credit Card. </summary>
    MasterCard,
}

/// <summary>
///     Credit Card class.
/// </summary>
internal sealed class CreditCard
{
    /// <summary>
    ///     Credit card's -> number.
    /// </summary>
    internal ulong CcNum { get; private set; }

    /// <summary>
    ///     Credit card's -> Expiry date.
    /// </summary>
    internal DateTime ExpiryDate { get; private set; }

    /// <summary>
    ///     Credit card's -> inserted Date.
    /// </summary>
    internal DateTime InsertedDate { get; private set; }

    /// <summary>
    ///     Credit card's -> Security code.
    /// </summary>
    internal string SecurityCode { get; private set; }

    /// <summary>
    ///     Credit card's -> name.
    /// </summary>
    internal string CcName { get; private set; }

    /// <summary>
    ///     Credit card's -> Type
    /// </summary>
    internal CcType CreditCardType { get; private set; }

    /// <summary>
    ///     Credit card's constructor
    /// </summary>
    private CreditCard()
    {
        CcName = default!;
        SecurityCode = default!;
    }

    /// <summary>
    ///     Credit card's constructor.
    /// </summary>
    /// <param name="ccNum"></param>
    /// <param name="expiryDate"></param>
    /// <param name="insertedDate"></param>
    /// <param name="securityCode"></param>
    /// <param name="ccName"></param>
    /// <param name="creditCardType"></param>
    internal CreditCard(ulong ccNum, DateTime expiryDate, DateTime insertedDate,
                        string securityCode, string ccName,
                        CcType creditCardType)
    {
        CcNum = ccNum;
        ExpiryDate = expiryDate;
        InsertedDate = insertedDate;
        SecurityCode = securityCode;
        CcName = ccName;
        CreditCardType = creditCardType;
    }

    /// <summary>
    ///     Insert Credit card to database.
    /// </summary>
    /// <param name="clientId">Client id</param>
    /// <param name="cc"> Credit card </param>
    /// <returns> And awaitable Tsk </returns>
    private static async Task<int>
    InsertCreditCardToDbAsync(Guid clientId, CreditCard cc) => await CmdExecuteNonQueryAsync(
        $"INSERT into creditcard (CcNum, ClientId, ExpiryDate, SecurityCode, NameInCC)" +
        $"WITH VALUES ({cc.CcNum}, {clientId}, {cc.ExpiryDate},{cc.InsertedDate},{cc.CcName}," +
        $"{cc.CreditCardType})");

    /// <summary>
    ///     SIMULATED CODE, to verify credit card is valid.
    /// </summary>
    /// <returns> Credit card type, awaitable. </returns>
    private static async Task<CcType> SomeBankingServiceToVerifyCc() =>
        //! TODO Some async method to verify credit card data,
        // outside the scope of our server.
        await Task<CcType>.Run( () => CcType.Visa);
    

    /// <summary>
    ///     Insert Credit card to database
    /// </summary>
    /// <param name="clientId"> Client id</param>
    /// <param name="cc"> Credit card</param>
    /// <returns></returns>
    /// <exception cref="DbInvalidDataException"> invalid data</exception>
    /// <exception cref="DuplicatePkException">duplicated </exception>
    internal static async Task<CcType> InsertCreditCardAsync(Guid clientId,
                                                        CreditCard cc)
    {
        try
        {
            CcType validation = await SomeBankingServiceToVerifyCc();

            if (validation == CcType.Invalid)
                return validation;

            if (await InsertCreditCardToDbAsync(clientId, cc) == 1)
            {
                return validation;
            }
            else
            {
                // Test the client Guid;
                if (await CmdExecuteQueryAsync<string>(
                        $"SELECT * FROM client WITH PRIMARY KEY = {clientId}") ==
                    clientId.ToString())
                {
                    throw new DbInvalidDataException(
                        "Specified client Guid does not exist");
                }
                // Duplicate credit card.
                throw new DuplicatePkException("Duplicate credit card");
            }
        }
        catch (DbInvalidDataException e)
        {
            Log.Error(e);
            return CcType.Invalid;
        }
    }

    /// <summary>
    ///     Get all from database
    /// </summary>
    /// <param name="clientId"> client Id</param>
    /// <returns> Query from database </returns>
    private static async Task<List<Dictionary<int, object?>>?>
    GetCLientCreditCardsFromDb(Guid clientId) => await CmdExecuteQueryAsync(
        $"SELECT * FROM creditcard WHERE ClientID == {clientId}");

    /// <summary>
    ///     Get all from database
    /// </summary>
    /// <param name="clientId"> client Id</param>
    /// <returns> A list of credit cards </returns>
    internal static async Task<List<CreditCard>?>
    GetClientCreditCards(Guid clientId)
    {
        try
        {
            var values = await GetCLientCreditCardsFromDb(clientId);
            var ccList = new List<CreditCard>();

            foreach (var (line, cc) in from line in values
                                       where values is not null
                                       let cc = new CreditCard()
                                       select (line, cc))
            {
                foreach (var val in from column in line
                                    where line is not null
                                    where column.Value is not null
                                    select column)
                {
                    switch (val.Key)
                    {
                        case 1:
                            cc.CcNum = (UInt64)val.Value;
                            break;
                        case 3:
                            cc.ExpiryDate = (DateTime)val.Value;
                            break;
                        case 4:
                            cc.InsertedDate = (DateTime)val.Value;
                            break;
                        case 5:
                            cc.SecurityCode = (string)val.Value;
                            break;
                        case 6:
                            cc.CcName = (string)val.Value;
                            break;
                    }
                }
                ccList.Add(cc);
            }

            return ccList;
        }
        catch (DataBaseException e)
        {
            Log.Error(e);
            return default;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }
}