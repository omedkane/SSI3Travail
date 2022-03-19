namespace DatabaseSimulator;

public interface IRow<Model> where Model : TableModel
{
	public Guid ID { get; }
	public Row<Model>? Update(string property, object value);
	public Row<Model> Update(Action<Model> updator);

}