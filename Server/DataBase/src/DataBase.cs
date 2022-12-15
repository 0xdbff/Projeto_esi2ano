using Npgsql;
using static Utils.Logger;

namespace DataBase;

/// <summary>
///
/// </summary>
public static class DataBase
{
    /// Absolute paths, This Files will be stored on the DBMS(postgres) server.
    #region bakup_path

    /// <summary> </summary>
    private static string? _backupDdlPath = "~/dev/repo_g06/data/";

    private static string? _backupDbPath = "~/dev/repo_g06/data/";

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
    private const string DbName = "IpcaGym";

    /// <summary> Admin Database name. /summary>
    private const string AdminDbName = "postgres";

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
        catch (Exception e)
        {
            Log.Error(e);
            return default;
        }
    }

    public static async Task<int> AdminCmdExecuteNonQueryAsync(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AdminConnString);
            await using var dataSource = dataSourceBuilder.Build();

            // Execute command(s) in the dbms and await results.
            await using var cmd = dataSource.CreateCommand(sql);
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
            // from an unknown query.
            var values = new List<Dictionary<int, object?>>();

            while (await reader.ReadAsync())
            {
                // A generic list to hold all the columns from the Table.
                var fieldList = new Dictionary<int, object?>();

                for (var currentField = 0; currentField < reader.FieldCount;
                     currentField++)
                {
                    fieldList.Add(currentField, reader.GetValue(currentField));
                    Console.WriteLine(reader.GetValue(currentField).GetType());
                }

                values.Add(fieldList);
            }

            return values;
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
    // public static async Task<object?> Get(string? sql)
    //{
    //     try
    //     {
    //         // Open a connection that will live through the execution of this
    //         method's
    //         // stack frame.
    //         await using var conn = new NpgsqlConnection(ConnString);
    //         await conn.OpenAsync();

    //        await using var cmd =
    //            new NpgsqlCommand($@"SELECT * FROM {tableName}", conn);
    //        await using var reader = await cmd.ExecuteReaderAsync();

    //        var a = reader.FieldCount;

    //        await reader.ReadAsync();

    //        return reader.GetValue(column);
    //    }
    //    catch (Exception e)
    //    {
    //        Log.Error(e);
    //        return default;
    //    }
    //}

    /// <summary>
    ///
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public static async Task<object?> RunSql(string? sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            await using var conn = new NpgsqlConnection(ConnString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);

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

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private static async Task ensureDataBaseTables()
    {
        try
        {
            // Data definition language (.ddl) file for IpcaGym.
            var ddl = await File.ReadAllTextAsync($"{_backupDdlPath}IpcaGym.ddl");

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
    /// Try creating a database on postgres server if one does not already exists,
    /// on failure exit the program.
    /// </summary>
    /// <returns> An awaitable Task. </returns>
    private static async Task createDatabaseAsync()
    {
        try
        {
            // Create a database
            await AdminCmdExecuteNonQueryAsync($"CREATE DATABASE {DbName}");
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
    ///
    /// </summary>
    /// <returns></returns>
    public static async Task Init()
    {
        try
        {
            // Open a connection 'Admin database' that will live through the execution
            // of this method's stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(AdminConnString);
            await using var dataSource = dataSourceBuilder.Build();

            // Test if there is a database with {DbName}
            await using var cmd = dataSource.CreateCommand(
                @$"SELECT datname FROM pg_catalog.pg_database 
                WHERE lower(datname) = lower('{DbName}');");
            await using var reader = await cmd.ExecuteReaderAsync();

            // Check database
            if (reader.Read())
            {
                // Ensure all the tables are present.
                await ensureDataBaseTables();
            }
            else
            {
                // Database does not exits, create one and Warn the admin.
                throw new NpgsqlException($"Database {DbName} Does not exist!!");
            }
        }
        catch (NpgsqlException e)
        {
            Log.Error(e);
            Log.Warn(
                $"Proceeding without database '{DbName}', no values are present...");
            Log.Warn(
                $"Creating a new one with name '{DbName}' as user {User}.");

            // Create Database.
            await createDatabaseAsync();
            // Create Tables
            await ensureDataBaseTables();
        }
        catch (Exception e)
        {
            Log.Error(e);
            //
        }
    }

    public static async Task Test()
    {
        // try
        // {
        //     // var insert = await Insert(@$"insert into
        //     // logindata(username,hashedpassword) VALUES ('username23',
        //     'hash')");
        //
        //     var values = await GetValues("select * from logindata")
        //         as List<Dictionary<int, object>>;
        //
        //     foreach (var val in from line in values
        //                         from collum in line
        //                         select
        //                  collum)
        //     {
        //         Console.WriteLine(val.Value);
        //     }
        // }
        // catch (Exception e)
        // {
        //     Log.Error(e);
        // }
        //
        try
        {
            // await using var conn = new NpgsqlConnection(ConnString);
            // await conn.OpenAsync();

            // Create the database if it does not exist.
            // await using var cmd = new NpgsqlCommand(@"create database
            // dbname", conn);

            await Init();
            // return (await cmd.ExecuteNonQueryAsync());
            // return 3;
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    #endregion
}
