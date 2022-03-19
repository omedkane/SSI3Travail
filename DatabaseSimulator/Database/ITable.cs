namespace DatabaseSimulator;

public interface ITable<Model> where Model : TableModel
{
	public void Insert(Model record);
	public void InsertAll(IEnumerable<Model> records);
	public Row<Model> Find(Guid id);
	public QueryResult<Row<Model>, Guid> FindAll(IEnumerable<Guid> listOfIDs);
	public List<Row<Model>> FindWhere(Func<Model, bool> where, int? limit);

	public Row<Model> FirstWhere(Func<Model, bool> where);
	public MutationResult<Guid, Guid> UpdateAll(IEnumerable<Guid> listOfIDs, Action<Model> updator);
	public List<Guid> UpdateWhere(Func<Model, bool> where, Action<Model> updator);

	public void Remove(Guid id);
	public List<Guid> RemoveWhere(Func<Model, bool> where);

	public List<Row<Model>> GetAll();
}