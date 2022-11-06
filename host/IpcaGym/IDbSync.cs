namespace host;

internal interface IDbSync
{
    internal Task Insert();

    internal Task Remove();

    internal Task Update();

    internal Task<Object> Get();

    internal Task<List<Object>> GetTable();
}
