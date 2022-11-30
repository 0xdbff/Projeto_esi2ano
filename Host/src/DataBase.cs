using NLog.Targets;
using Npgsql;
using System.Data;
using static Host.Utils;
using System.Linq;

namespace Host;

/// <summary>
/// 
/// </summary>
internal static class DataBase
{
    /// <summary>
    ///     Connection String to system database (currently using postgres 14+).
    ///     Any Change to the Database connection rules will require a new
    ///     binary file with updated values as this constants are not intended
    ///     to change during runtime.
    /// </summary>
    private const string ConnString =
        $@"Server={Host};Username={User};Database={DbName};Port={Port};Password={PassWord};SSLMode=Prefer";

    #region connection_rules

    /// <summary> Hostname for postgresql server. </summary>
    private const string Host = "localhost";

    /// <summary> Username for postgresql server. </summary>
    private const string User = "postgres";

    /// <summary> Database name for postgresql server. </summary>
    private const string DbName = "testdb";

    /// <summary>
    ///     Password for postgresql server (needless to say, protect this src
    ///     file as well as the generated code).
    /// </summary>
    private const string PassWord = "IpcaGymPa$$word!";

    /// <summary>
    ///     Network Port for postgresql server, other ports may be blocked...
    /// </summary>
    private const string Port = "5432";

    #endregion

    #region methods

    /// <summary>
    ///     Initialize a database connection.
    ///     Connections must be disposed when they are no longer needed - not
    ///     doing so can result in a connection leak, which can crash your
    ///     program, this is done via the await using C# construct, which ensures
    ///     that the connection is disposed even if an exception is later thrown.
    /// </summary>
    /// <returns>
    ///     Number of rows affected.
    /// </returns>
    internal static async Task<int> Insert(string? sql)
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnString);
            await conn.OpenAsync();

            // @ allows escape characters.
            await using var cmd = new NpgsqlCommand($@"INSERT INTO {sql}", conn);

            return (await cmd.ExecuteNonQueryAsync());
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    internal static async Task<List<List<object?>>?> GetAll(string? tableName)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            await using var conn = new NpgsqlConnection(ConnString);
            await conn.OpenAsync();

            await using var cmd =
                new NpgsqlCommand($@"SELECT * FROM {tableName}", conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            // A generic list to hold all the lines from the Table.
            var tableList = new List<List<object?>>();

            // Read every value with it's matching type, add it to the field list
            // and in the outer loop to the table list.
            while (await reader.ReadAsync())
            {
                // A generic list to hold all the columns from the Table.
                var fieldList = new List<object?>();

                for (var currentField = 0; currentField < reader.FieldCount;
                     currentField++)
                    fieldList.Add(reader.GetValue(currentField));

                tableList.Add(fieldList);
            }

            return tableList;
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    internal static async Task<object?> Get(string? tableName, int column)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            await using var conn = new NpgsqlConnection(ConnString);
            await conn.OpenAsync();

            await using var cmd =
                new NpgsqlCommand($@"SELECT * FROM {tableName}", conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            var a = reader.FieldCount;

            await reader.ReadAsync();
            return reader.GetValue(column);
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    internal static async Task<object?> RunSql(string? sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            await using var conn = new NpgsqlConnection(ConnString);
            await conn.OpenAsync();

            await using var cmd =
                new NpgsqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            var a = reader.FieldCount;

            await reader.ReadAsync();
            return reader.GetValue(0);
        }
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    public static async Task Test()
    {
        try
        {
            var values = await GetAll("logindata");
            foreach (var value in from line in values
                                  from collum in line
                         select collum
                    )
            {
                Console.WriteLine(value);
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    #endregion
}