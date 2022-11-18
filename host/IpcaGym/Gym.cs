namespace host;

/// <summary>
/// 
/// </summary>
public class Gym : IDataSync<Gym.DbTable>
{

    private uint _numClientes { get; set; }
    private uint _numFuncionarios { get; set; }
    private uint _LotacaoTotal { get; set; }
    public uint LotacaoTotal { get; set; }

    internal struct DbTable
    {
        int a;
        int b;
    }
    
    Task<int> IDataSync<DbTable>.Insert<T>(in T Table)
    {
        throw new NotImplementedException();
    }

    Task<int> IDataSync<DbTable>.Remove<T>(in T Table)
    {
        throw new NotImplementedException();
    }

    Task<int> IDataSync<DbTable>.Update<T>(in T Table)
    {
        throw new NotImplementedException();
    }

    Task<List<object>>
        IDataSync<DbTable>.GetWithPk<T>(in string? tableName,
                                        params string?[] primaryKeys)
    {
        throw new NotImplementedException();
    }

    Task<List<object>>
        IDataSync<DbTable>.GetFieldWithPk<T>(in string? tableName, int column,
                                             params string?[] primaryKeys)
    {
        throw new NotImplementedException();
    }

    Task<List<List<object>>>
        IDataSync<DbTable>.GetTable<T>(in string? tableName)
    {
        throw new NotImplementedException();
    }
}