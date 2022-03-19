using DatabaseSimulator.Exceptions;
using DatabaseSimulator.Miscellaneous;

namespace DatabaseSimulator;

public class Row<Model> : IRow<Model> where Model : TableModel
{
	public Guid ID { get => Record.ID; }
	public readonly Model Record;
	internal Row(Model record)
	{
		Record = record;
	}

	public Row<Model> Update(string property, object value)
	{
		if (property == "ID" || property == "_ID")
			throw new PrimaryKeyException("Error: Cannot modify a record's primary key !");

		ObjectHelper.Set(Record, property, value);
		return this;

	}

	public Row<Model> Update(Action<Model> updator)
	{
		updator(Record);
		return this;
	}

	// ! Rules
}
