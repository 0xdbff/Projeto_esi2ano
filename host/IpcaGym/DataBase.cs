using Npgsql;

namespace host;

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
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        // @ allows escape characters.
        await using var cmd = new NpgsqlCommand($"INSERT INTO {sql}", conn);

        return (await cmd.ExecuteNonQueryAsync());
    }

    /// <summary>
    ///
    /// </summary>
    internal static async Task<List<Object>> GetTable(string? tableName)
    {
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand($"SELECT * FROM {tableName}", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        // A generic list to hold all the values from the table fields.
        var fieldList = new List<Object>();

        // Read every value with it's matching type, and add it to the list.
        while (await reader.ReadAsync())
            fieldList.Add($"{reader.GetValue(2)}");

        return fieldList;
    }

    internal static async Task<Object> Get(string? tableName, int pos)
    {
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand($"SELECT * FROM {tableName}", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        var a = reader.FieldCount;

        await reader.ReadAsync();
        return reader.GetValue(pos);
    }

    public static async Task test()
    {
        // await Insert("COMPANY VALUES (1, 'Paul', 32, 'California', 20000.00)");

        var l = await GetTable("COMPANY");

        foreach (var val in l)
        {
            Console.WriteLine($"{val}");
        }

        // var val2 = await Get("test", 0);
        //
        // Console.WriteLine(val2);
    }

    #endregion
}
