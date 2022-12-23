using Npgsql;
using static Utils.Logger;

namespace Data;

/// <summary>
///
/// </summary>
public static class DataBase
{
    /// Absolute paths, This Files will be stored on the DBMS(postgres) server.
    #region bakup_path

    /// <summary> </summary>
    private static string? backupDdlPath = "/home/db/dev/repo_g06/Data/dataDefinition/";

    private static string? backupDbPath = "/home/db/dev/repo_g06/Data/backup/";

    #endregion

    #region connection_rules

    /// <summary>
    ///     Connection String to system's database (currently using postgres 14+).
    ///     Any Change to the Database connection rules will require a new
    ///     binary file with updated values as this constants are not intended
    ///     to change during runtime.
    /// </summary>
    private const string ConnString =
        $@"Server={Host};Username={User};Database={DbName};Port={Port};
        Password={PassWord};SSLMode=Prefer";

    /// <summary>
    ///     Connection String to system's database as an administrator for the
    ///     DBMS(currently using postgres 14+).
    ///     Any Change to the Database connection rules will require a new
    ///     binary file with updated values as this constants are not intended
    ///     to change during runtime.
    /// </summary>
    private const string AdminConnString =
        $@"Server={Host};Username={User};Database={AdminDbName};Port={Port};
        Password={PassWord};SSLMode=Prefer";

    /// <summary> Hostname for postgresql server. </summary>
    private const string Host = "localhost";

    /// <summary> Username for postgresql server. </summary>
    private const string User = "postgres";

    /// <summary> Database name. </summary>
    private const string DbName = "ipcagym";

    /// <summary> Admin Database name. /summary>
    private const string AdminDbName = "postgres";

    //! TODO change auth method!

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
    public static async Task<int> CmdExecuteNonQueryAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            await using var dataSource = dataSourceBuilder.Build();

            // Execute command(s) in the dbms and await results.
            await using var cmd = dataSource.CreateCommand(sql);
            return (await cmd.ExecuteNonQueryAsync());
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    private static async Task<int> AdminCmdExecuteNonQueryAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame with the Admin Database.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AdminConnString);
            await using var dataSource = dataSourceBuilder.Build();

            // Execute command(s) in the dbms and await results.
            await using var cmd = dataSource.CreateCommand(sql);
            return (await cmd.ExecuteNonQueryAsync());
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static async Task<List<Dictionary<int, object?>>?>
    CmdExecuteQueryAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            // A List of Dictionary with key value pairs, to hold return values
            // from a query.
            var values = new List<Dictionary<int, object?>>();

            while (await reader.ReadAsync())
            {
                // A generic Dictionary to hold all the columns from the Table.
                var columns = new Dictionary<int, object?>();

                foreach (var currentField in Enumerable.Range(0, reader.FieldCount))
                    columns.Add(currentField, reader.GetValue(currentField));

                values.Add(columns);
            }

            return values;
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static async Task<Dictionary<int, object?>?>
    CmdExecuteQuerySingleAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            // A generic Dictionary to hold all the columns from the Table.
            var fieldList = new Dictionary<int, object?>();

            while (await reader.ReadAsync())
            {
                foreach (var currentField in Enumerable.Range(0, reader.FieldCount))
                    fieldList.Add(currentField, reader.GetValue(currentField));
            }

            return fieldList;
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<T?> CmdExecuteQueryAsync<T>(string? @sql)
        where T : System.IConvertible
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return await reader.GetFieldValueAsync<T?>(0);

            // Query with no value
            throw new DataBaseException("No data");
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            throw new Exception("No data");
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    private static async Task<List<Dictionary<int, object?>>?>
    AdminCmdExecuteQueryAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame with the Admin Database.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AdminConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            // A List of Dictionary with key value pairs, to hold return values
            // from a querie (every line).
            var values = new List<Dictionary<int, object?>>();

            while (await reader.ReadAsync())
            {
                // A generic Dictionary to hold all the columns from the Table.
                var columns = new Dictionary<int, object?>();

                foreach (var currentField in Enumerable.Range(0, reader.FieldCount))
                    columns.Add(currentField, reader.GetValue(currentField));

                values.Add(columns);
            }

            return values;
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sql"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static async Task<T?> AdminCmdExecuteQueryAsync<T>(string? @sql)
        where T : System.IConvertible
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame with the Admin Database.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AdminConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return await reader.GetFieldValueAsync<T?>(0);

            // Query with no value
            throw new DataBaseException("No data");
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            return default;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private static async Task ensureDataBaseTables()
    {
        try
        {
            // Data definition language (.ddl) file for IpcaGym.
            var ddl = await File.ReadAllTextAsync($"{backupDdlPath}IpcaGym.ddl");

            // !TODO duplicate constrains produce an error.
            // Execute all the commands previously defined.
            await CmdExecuteQueryAsync(ddl);
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            Log.Error("Cannot proceed, exiting program with exit code 1");

            // Exit the executable with an error code.
            Environment.Exit(1);
        }
    }

    /// <summary>
    ///     Try creating a database on postgres server if one does not already
    ///     exists, on failure exit the program.
    /// </summary>
    /// <returns> An awaitable Task. </returns>
    /// <exception cref="Exception"></exception>
    private static async Task<int?> createDatabaseAsync()
    {
        try
        {
            // Create a database
            return await AdminCmdExecuteNonQueryAsync($"CREATE DATABASE {DbName}");
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            Log.Error("Cannot proceed, exiting program with exit code 1");

            // Exit the executable with an error code.
            throw new Exception("Fatal, there are no databases!");
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static async Task Init()
    {
        try
        {
            // Test if there is a database with {DbName}
            var queryReturn = await AdminCmdExecuteQueryAsync<string>(
                $"SELECT datname FROM pg_catalog.pg_database " +
                $"WHERE lower(datname) = lower('{DbName}')");

            Console.WriteLine(queryReturn);

            if (queryReturn == DbName.ToLower())
            {
                // Ensure database tables.
                // !TODO change .ddl file -> Constrains cannot be redefined
                // without a drop.
                // await ensureDataBaseTables();

                return;
            }

            throw new NpgsqlException($"Database {DbName} does not exist");
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            Log.Warn(
                $"Proceeding without database '{DbName}', no values are present...");
            Log.Warn($"Creating a new one with name '{DbName}' as user {User}.");

            // Create Database.
            await createDatabaseAsync();

            // Create Tables
            await ensureDataBaseTables();

            // Try Loading database backups if there are any.
            Log.Warn($"Attempting to load DataBase backups");
            //! TODO
        }
        catch (Exception e)
        {
            Log.Error(e);
            Log.Error("Cannot proceed, exiting program with exit code 1");

            // Exit the executable with an error code.
            Environment.Exit(1);
        }
    }

    #endregion
}
