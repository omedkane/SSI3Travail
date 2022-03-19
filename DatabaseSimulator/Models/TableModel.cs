using DatabaseSimulator.Exceptions;

namespace DatabaseSimulator;
public abstract class TableModel
{
	private Guid _ID;
	public Guid ID
	{
		get => _ID;
		set
		{
			if (_ID.Equals(Guid.Empty))
			{
				_ID = value;
			}
			else
				throw new PrimaryKeyException();
		}
	}
	public TableModel() { }
	public TableModel(Guid id)
	{
		ID = id;
	}
}