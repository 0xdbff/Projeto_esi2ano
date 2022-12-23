namespace Host.Exceptions;

/// <summary>
/// 
/// </summary>
internal class ClientException : UserException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    internal ClientException(string message) : base(message)
    {
    }
}

/// <summary>
/// 
/// </summary>
internal class InvalidClientDataException : ClientException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    internal InvalidClientDataException(string message) : base(message)
    {
        
    }
}
