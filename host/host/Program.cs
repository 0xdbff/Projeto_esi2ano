using Npgsql;

var connString = "Host=localhost:5432;Username=admin;Password=S$0m*Pa$$Wor|d!;Database=testdb";

await using var conn = new NpgsqlConnection(connString);
await conn.OpenAsync();

// Insert some data
await using (var cmd = new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", conn))
{
    cmd.Parameters.AddWithValue("Test nas Bd");
    await cmd.ExecuteNonQueryAsync();
}

Console.WriteLine($"{conn}");

// Retrieve all rows
await using (var cmd = new NpgsqlCommand("SELECT name FROM test", conn))
await using (var reader = await cmd.ExecuteReaderAsync())
{
    while (await reader.ReadAsync())
    {
        Console.WriteLine(reader.GetString(0));
    }
}