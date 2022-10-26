using Npgsql;

var connString = "Host=localhost:5432;Username=postgres;Password=IpcaGymPa$$word!;Database=testdb";

await using var conn = new NpgsqlConnection(connString);
await conn.OpenAsync();

// Insert some data
await using (var cmd = new NpgsqlCommand("INSERT INTO test (name) VALUES ($1)", conn))
{
    cmd.Parameters.AddWithValue("...");
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