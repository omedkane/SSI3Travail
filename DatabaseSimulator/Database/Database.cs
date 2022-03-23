namespace DatabaseSimulator;

using DatabaseSimulator.Exceptions;
using DatabaseSimulator.Miscellaneous;

public class Database : IDatabase
{
	private readonly Dictionary<Type, dynamic>
	Tables = new Dictionary<Type, dynamic>();

	private Table<T> TableFromRow<T>(Row<T> row)
		where T : TableModel => new Table<T>(row);

	private Table<T> TableFromModel<T>(T record)
		where T : TableModel => TableFromRow<T>(new Row<T>(record));

	public Table<T> CreateTable<T>(T model) where T : TableModel
	{
		var _table = TableFromModel(model);

		Tables.Add(typeof(T), _table);

		return _table;
	}
	public Table<NewModel> Update<OldModel, NewModel>()
	where NewModel : TableModel, new()
	where OldModel : TableModel
	{
		Type oldModel = typeof(OldModel);
		Type newModel = typeof(NewModel);

		Table<OldModel> oldTable = Tables[oldModel];
		List<Row<OldModel>> oldRows = oldTable.GetAll();

		OldModel oldFirstRecord = oldRows.First().Record;
		NewModel newFirstRecord = ObjectHelper.CopyTo<OldModel, NewModel>(oldFirstRecord, new NewModel(), oldModel, newModel);

		Table<NewModel> newTable = CreateTable<NewModel>(newFirstRecord);


		int iteration = 0;
		foreach (Row<OldModel> row in oldRows)
		{
			if (iteration == 0)
			{
				iteration++;
				continue;
			}
			NewModel newRecord = ObjectHelper.CopyTo<OldModel, NewModel>(row.Record, new NewModel(), oldModel, newModel);
			newTable.Insert(newRecord);
		}

		Tables.Remove(oldModel);

		return newTable;
	}



	public void Remove<T>() where T : TableModel
	{
		Tables.Remove(typeof(T));
	}

	public Table<T> GetTable<T>() where T : TableModel
	{
		try
		{
			return Tables[typeof(T)];
		}
		catch (System.Exception)
		{
			throw new InexistantTableException("Error: This table doesn't exists !");
		}
	}

}