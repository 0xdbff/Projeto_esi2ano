using Host;
using Host.Exceptions;
using static Host.Utils;

namespace Host;

/// <summary>
/// 
/// </summary>
internal enum ClientType
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
internal enum Bmi
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
internal partial class Client : Person, ILogin
{
    #region attributes

    /// <summary>
    /// The type of the client assigned by the system.
    /// </summary>
    internal ClientType ClientType { get; private set; }

    /// <summary>
    /// The client's height in meters.
    /// </summary>
    private double _height;

    /// <summary>
    /// The client's weight in kilograms.
    /// </summary>
    private double _weight;

    /// <summary>
    /// The client's BMI value.
    /// </summary>
    internal double BmiValue 
    { 
        get => _weight / (_height * _height);
    }

    /// <summary>
    /// Get current Bmi situation, for the average person.
    /// </summary>
    internal Bmi CurrentBmi
    {
        get => BmiValue switch
        {
            < 3.5 => throw new InvalidClientDataException("BMI not valid, too small"),
            < 16 => Bmi.Underweight3,
            < 17 => Bmi.Underweight2,
            < 18.5 => Bmi.Underweight1,
            < 25 => Bmi.Normal,
            < 30 => Bmi.Overweight,
            < 35 => Bmi.Obese1,
            < 40 => Bmi.Obese2,
            < 100 => Bmi.Obese3,
            _ => throw new InvalidClientDataException("BMI not valid, too high")
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


    #region methods

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
            _weight = weight < 200 && weight > 20
                ? weight
                : throw new InvalidClientDataException("Weight is not Valid");
        }
        catch (InvalidClientDataException e)
        {
            Log.Error(e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static List<double>? GetImcHistory()
    {
        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    ILogin.LoginStatus ILogin.Login()
    {
        throw new NotImplementedException();
    }

    #endregion
}