namespace Data;
using Npgsql;

/// <summary>
///     An Exception That inherits from NpgsqlException.
///     Represents an Exception from a DataBase
/// </summary>
public class DataBaseException : NpgsqlException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    public DataBaseException(string message) : base(message) { }
}

/// <summary>
///     An Exception That inherits from DataBaseException.
///     Represents an Exception from a DataBase when invalid queries
///     are used.
/// </summary>
public class DbInvalidDataException : DataBaseException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    public DbInvalidDataException(string message) : base(message) { }
}

/// <summary>
///     An Exception That inherits from DbInvalidDataException.
///     Represents an Exception from a DataBase in particular
///     for this Exception a duplicate primary fails on insertion.
/// </summary>
public class DuplicatePkException : DbInvalidDataException
{
    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="message"> Exception message </param>
    public DuplicatePkException(string message) : base(message) { }
}
