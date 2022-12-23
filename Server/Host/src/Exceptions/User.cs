namespace Host.Exceptions;

/// <summary>
///     User Exceptions.
/// </summary>
internal class UserException : Exception
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    internal UserException(string message) : base(message)
    {
    }
}

/// <summary>
///     An Exception when an invalid User is found.
/// </summary>
internal class InvalidUserException : UserException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    internal InvalidUserException(string message) : base(message)
    {
    }
}
