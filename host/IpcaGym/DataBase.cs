using System.ComponentModel;
using Npgsql;

namespace host;

internal class DataBase
{
    #region default_values

    /// <summary> Hostname for postgresql server. </summary>
    private const string Host = "localhost";

    /// <summary> Username for postgresql server. </summary>
    private const string User = "postgres";

    /// <summary> Database name for postgresql server. </summary>
    private const string DbName = "testdb";

    /// <summary>
    /// Password for postgresql server (needless to say, protect this src code file as well as the generated bin).
    /// </summary>
    private const string PassWord = "IpcaGymPa$$word!";

    /// <summary> Network Port for postgresql server, other ports may be blocked... </summary>
    private const string Port = "5432";

    #endregion
    
    /// <summary>
    /// Connection String to system database (currently using postgres 14+).
    ///
    /// Any Changes to the Database connection rules will require a new binary file with updated default_values
    /// as this constants are not intended to change during runtime.
    /// </summary>
    private const string ConnString =
        $"Server={Host};Username={User};Database={DbName};Port={Port};Password={PassWord};SSLMode=Prefer";

    /// <summary>
    /// Initialize a database connection.
    /// 
    /// Connections must be disposed when they are no longer needed - not doing so can result
    /// in a connection leak, which can crash your program, this is done via the await using C# construct,
    /// which ensures the connection is disposed even if an exception is later thrown
    /// </summary>
    /// <returns></returns>
    internal static async Task Add()
    {
        await using var conn = new NpgsqlConnection(ConnString);
        
        await conn.OpenAsync();
        
        await using var cmd1 = new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", conn);
        
        cmd1.Parameters.AddWithValue("...");
        await cmd1.ExecuteNonQueryAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT name FROM test", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
            Console.WriteLine(reader.GetString(0));
    }
    
    internal static async Task Read()
    {
        await using var conn = new NpgsqlConnection(ConnString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT name FROM test", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
            Console.WriteLine(reader.GetString(0));
    }

    public static async Task Test()
    {
        await Add();
        await Read();
    }
}