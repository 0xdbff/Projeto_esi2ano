namespace Data;
using Npgsql;

/// <summary>
/// 
/// </summary>
public class DataBaseException : NpgsqlException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public DataBaseException(string message) : base(message) { }
}

/// <summary>
/// 
/// </summary>
public class DbInvalidDataException : DataBaseException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public DbInvalidDataException(string message) : base(message) { }
}

/// <summary>
/// 
/// </summary>
public class DuplicatePkException : DbInvalidDataException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public DuplicatePkException(string message) : base(message) { }
}
