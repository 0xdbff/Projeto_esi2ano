namespace Host;

/// <summary>
/// Interface for the database synchronization with the server code at runtime.
/// </summary>
/// <typeparam name="TData"></typeparam>
internal interface IDataSync<TData>
{
    internal Task<int> Insert(in TData table);

    internal Task<int> Remove(in TData table);

    internal Task<int> Update(in TData table);

    internal Task<List<TData>> GetWithPk<T>(in string? tableName,
                                             params string?[] primaryKeys);

    internal Task<List<TData>> GetFieldWithPk<T>(in string? tableName, int column,
                                                  params string?[] primaryKeys);
}