using Npgsql;

namespace host;

internal class DataBase
{
    /// <summary> Hostname for postgresql server. </summary>
    static string _host = "mydemoserver.postgres.database.azure.com";

    /// <summary> Hostname for postgresql server. </summary>
    static string _user = "mylogin@mydemoserver";

    /// <summary> Hostname for postgresql server. </summary>
    static string _dbName = "mypgsqldb";

    /// <summary> Hostname for postgresql server. </summary>
    static string _passwHash = "<server_admin_password>";

    /// <summary> Hostname for postgresql server. </summary>
    static string _port = "5432";


    /// <summary>
    /// Initialize database connection.
    /// 
    /// Connections must be disposed when they are no longer needed - not doing so can result
    /// in a connection leak, which can crash your program, this is done via the await using C# construct,
    /// which ensures the connection is disposed even if an exception is later thrown
    /// </summary>
    private static async Task _init()
    {
        const string connString = "Host=localhost:5432;Username=postgres;Password=IpcaGymPa$$word!;Database=testdb";

        await using var conn = new NpgsqlConnection(connString);

        await conn.OpenAsync();
        // await conn.OpenAsync();

        await using var cmd1 = new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", conn);

        cmd1.Parameters.AddWithValue("...");
        await cmd1.ExecuteNonQueryAsync();

        await using var cmd = new NpgsqlCommand("SELECT name FROM test", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            Console.WriteLine(reader.GetString(0));
    }

    public static async Task Test()
    {
        // await Insert();
        // await Get();
        await _init();
    }

    // static async Task Insert()
    // {
    //     await _init();
    //     await using var cmd = new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", _connection);
    //     
    //     cmd.Parameters.AddWithValue("...");
    //     await cmd.ExecuteNonQueryAsync();
    // }
    //
    // static async Task Get()
    // {
    //     await _init();
    //     await using var cmd = new NpgsqlCommand("SELECT name FROM test", _connection);
    //     await using var reader = await cmd.ExecuteReaderAsync();
    //     
    //     while (await reader.ReadAsync())
    //         Console.WriteLine(reader.GetString(0));
    //     
    // }
}