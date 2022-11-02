using Npgsql;

namespace host;

internal class DataBase
{
    /// <summary>
    ///     Connection String to system database (currently using postgres 14+).
    ///     Any Changes to the Database connection rules will require a new
    ///     binary file with updated values as this constants are not intended
    ///     to change during runtime.
    /// </summary>
    private const string ConnString =
        $"Server={Host};Username={User};Database={DbName};Port={Port};Password={PassWord};SSLMode=Prefer";

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
    /// <returns> Number of rows affected. </returns>
    internal static async Task<int> Add()
    {
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        await using var cmd =
            new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", conn);
        cmd.Parameters.AddWithValue("...");
        
        return await cmd.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    internal static async Task Read()
    {
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM test", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            Console.WriteLine(reader.GetString(0));
    }

    public static async Task Test()
    {
        await Add();
        await Read();
    }

    #endregion
}