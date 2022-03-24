namespace DatabaseSimulator;

public class Table<Model> : ITable<Model> where Model : TableModel
{
    private readonly Dictionary<Guid, Row<Model>> Rows = new Dictionary<Guid, Row<Model>>();

    internal Table(Row<Model> record) => Rows.Add(record.ID, record);

    public int Count
    {
        get => Rows.Count;
    }

    public void Insert(Model record) => Rows.Add(record.ID, new Row<Model>(record));

    public void InsertAll(IEnumerable<Model> records)
    {
        foreach (Model record in records)
            Insert(record);
    }

    public Row<Model> Find(Guid id) => Rows[id];

    public QueryResult<Row<Model>, Guid> FindAll(IEnumerable<Guid> listOfIDs)
    {
        QueryResult<Row<Model>, Guid> queryResult = new QueryResult<Row<Model>, Guid>();
        foreach (Guid id in listOfIDs)
        {
            try
            {
                Row<Model> row = Rows[id];
                queryResult.AddFound(row);
            }
            catch (System.Exception)
            {
                queryResult.AddNotFound(id);
            }
        }
        return queryResult;
    }

    public List<Row<Model>> FindWhere(Func<Model, bool> where, int? limit = null)
    {
        List<Row<Model>> rows = Rows.Values.ToList();
        List<Row<Model>> results = new List<Row<Model>>();

        foreach (Row<Model> row in rows)
        {
            if (where(row.Record))
            {
                results.Add(row);
                if (limit is not null && results.Count == limit)
                    break;
            }
        }

        return results;
    }

    public Row<Model> FirstWhere(Func<Model, bool> where)
    {
        return FindWhere(where, 1).First();
    }

    public MutationResult<Guid, Guid> UpdateAll(IEnumerable<Guid> listOfIDs, Action<Model> updator)
    {
        MutationResult<Guid, Guid> result = new MutationResult<Guid, Guid>();

        foreach (Guid id in listOfIDs)
        {
            try
            {
                Row<Model> row = Rows[id];

                row.Update(updator);

                result.AddSuccess(id);
            }
            catch (System.Exception)
            {
                result.AddFailure(id);
            }
        }

        return result;
    }

    public List<Guid> UpdateWhere(Func<Model, bool> where, Action<Model> updator)
    {
        List<Guid> result = new List<Guid>();

        FindWhere(where)
            .ForEach(
                row =>
                {
                    row.Update(updator);
                    result.Add(row.ID);
                }
            );

        return result;
    }

    public List<Row<Model>> GetAll() => Rows.Values.ToList();

    public void Remove(Guid id) => Rows.Remove(id);

    public List<Guid> RemoveWhere(Func<Model, bool> where)
    {
        List<Row<Model>> targetRows = FindWhere(where);
        List<Guid> removedRows = new List<Guid>();
        foreach (Row<Model> row in targetRows)
        {
            Rows.Remove(row.ID);
            removedRows.Add(row.ID);
        }

        return removedRows;
    }
}
