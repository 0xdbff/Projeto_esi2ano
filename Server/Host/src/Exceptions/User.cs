namespace Host.Exceptions;

/// <summary>
/// 
/// </summary>
internal class UserException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    internal UserException(string message) : base(message)
    {
    }
}

/// <summary>
/// 
/// </summary>
internal class InvalidUserException : UserException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    internal InvalidUserException(string message) : base(message)
    {
    }
}
