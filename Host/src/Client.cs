using Host;
using Host.Exceptions;
using static Host.Utils;

namespace Host;

/// <summary>
/// 
/// </summary>
public enum ClientType
{
    /// <summary> </summary>
    Academic,
    /// <summary> </summary>
    Common,
    /// <summary> </summary>
    Invalid
}

/// <summary>
/// 
/// </summary>
public enum Imc
{
    /// <summary> </summary>
    Underweight3,
    /// <summary> </summary>
    Underweight2,
    /// <summary> </summary>
    Underweight1,
    /// <summary> </summary>
    Normal,
    /// <summary> </summary>
    Overweight,
    /// <summary> </summary>
    Obese1,
    /// <summary> </summary>
    Obese2,
    /// <summary> </summary>
    Obese3,
}

/// <summary>
/// 
/// </summary>
internal class Client : Person, ILogin
{
    #region attributes

    /// <summary>
    /// 
    /// </summary>
    private protected ClientType _clientType;

    /// <summary>
    /// 
    /// </summary>
    private protected double _height;

    /// <summary>
    /// 
    /// </summary>
    private protected double _weight;

    /// <summary>
    /// 
    /// </summary>
    internal double ImcValue
    { get => _weight / (_height * _height); }

    /// <summary>
    /// Get current Imc situation, for the average person.
    /// </summary>
    internal Imc CurrentImc
    {
        get => ImcValue switch
        {
            < 16 => Imc.Underweight3,
            < 17 => Imc.Underweight2,
            < 18.5 => Imc.Underweight1,
            < 25 => Imc.Normal,
            < 30 => Imc.Overweight,
            < 35 => Imc.Obese1,
            < 40 => Imc.Obese2,
            _ => Imc.Obese3
        };
    }

    #endregion

    /// <summary>
    /// Register a new client.
    /// Client's class constructor.
    /// </summary>
    public Client()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    List<ILogin.UsedCredentials> ILogin.GetCredentialsHistory()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    List<ILogin.Data> ILogin.GetLoginHistory()
    {
        throw new NotImplementedException();
    }

    #region methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal Imc EvaluateImc()
    {
        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="height"></param>
    internal void UpdateHeight(double height)
    {
        try
        {
            _height = height < 2.4 && height > 1.0
                ? height
                : throw new InvalidClientDataException("Height is not Valid");
        }
        catch (InvalidClientDataException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weight"></param>
    internal void UpdateWeight(double weight)
    {
        try
        {
            _height = weight < 200 && weight > 20
                ? weight
                : throw new InvalidClientDataException("Weight is not Valid");
        }
        catch (InvalidClientDataException e)
        {
            Log.Error(e);
        }
    }

    #endregion
}