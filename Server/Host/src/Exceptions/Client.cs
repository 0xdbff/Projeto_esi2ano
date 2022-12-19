namespace Host.Exceptions;

/// <summary>
/// 
/// </summary>
internal class ClientException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
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
    /// 
    /// </summary>
    /// <param name="message"></param>
    internal InvalidClientDataException(string message) : base(message)
    {
        
    }
}
