using NLog.Targets;
using Npgsql;
using System.Data;
using static Host.Utils;
using System.Linq;
using System.Collections.Generic;

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
    internal static async Task<int> Insert(string? @sql)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal static async Task<List<Dictionary<int, object?>>?> GetValues(string? @sql)
    {
        try
        {
            // Open a connection that will live through the execution of this method's
            // stack frame.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            await using var dataSource = dataSourceBuilder.Build();

            await using var cmd = dataSource.CreateCommand(sql);
            await using var reader = await cmd.ExecuteReaderAsync();

            // A generic list to hold all possible lines from the Table.
            var values = new List<Dictionary<int, object?>>();

            while (await reader.ReadAsync())
            {
                // A generic list to hold all the columns from the Table.
                var fieldList = new Dictionary<int, object?>();

                for (var currentField = 0; currentField < reader.FieldCount;
                     currentField++)
                { 
                    fieldList.Add(currentField, reader.GetValue(currentField));
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
    //internal static async Task<object?> Get(string? sql)
    //{
    //    try
    //    {
    //        // Open a connection that will live through the execution of this method's
    //        // stack frame.
    //        await using var conn = new NpgsqlConnection(ConnString);
    //        await conn.OpenAsync();

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

            //
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
            //var insert = await Insert(@$"insert into logindata(username,hashedpassword) VALUES ('username23', 'hash')");

            var values = await GetValues("select * from logindata") as List<Dictionary<int,object>>;

            foreach (var val in from line in values
                                  from collum in line
                                  select collum
                    )
            {
                Console.WriteLine(val.Value);
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    #endregion
}