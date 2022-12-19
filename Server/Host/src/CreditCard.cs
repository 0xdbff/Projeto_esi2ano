namespace Host;

using Data;
using static Utils.Logger;
using static Data.DataBase;

/// <summary>
/// 
/// </summary>
internal class CreditCard
{
    /// <summary>
    /// 
    /// </summary>
    internal UInt64 CcNum { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal DateTime ExpiryDate { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal DateTime InsertedDate { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal string SecurityCode { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal string CcName { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    internal CcType CreditCardType { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    private CreditCard()
    {
        CcName = default!;
        SecurityCode = default!;
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="cc"></param>
    /// <returns></returns>
    private static async Task<int>
    InsertCreditCardToDb(Guid clientId, CreditCard cc) => await CmdExecuteNonQueryAsync(
        $"INSERT into creditcard (CcNum, ClientId, ExpiryDate, SecurityCode, NameInCC)" +
        $"WITH VALUES ({cc.CcNum}, {clientId}, {cc.ExpiryDate},{cc.InsertedDate},{cc.CcName}," +
        $"{cc.CreditCardType})");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="cc"></param>
    /// <returns></returns>
    /// <exception cref="DbInvalidDataException"></exception>
    /// <exception cref="DuplicatePkException"></exception>
    internal static async Task<CcType> InsertCreditCard(Guid clientId,
                                                        CreditCard cc)
    {
        try
        {                              // !TODO
            const CcType validation = CcType.Visa; // Some method to verify credit card
                                             // data, outside the scope of our server
            if (validation == CcType.Invalid)
                return validation;

            if (await InsertCreditCardToDb(clientId, cc) == 1)
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
        catch (DuplicatePkException e)
        {
            Log.Error(e);
            return CcType.Invalid;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    private static async Task<List<Dictionary<int, object?>>?>
    GetCLientCreditCardsFromDb(Guid clientId) => await CmdExecuteQueryAsync(
        $"SELECT * FROM creditcard WHERE ClientID == {clientId}");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
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
