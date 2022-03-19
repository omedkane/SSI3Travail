namespace DatabaseSimulator.Exceptions;
public class ValueTypeMismatchException : Exception
{
	public ValueTypeMismatchException() { }
	public ValueTypeMismatchException(string message) : base(message) { }
}
public class InexistantPropertyException : Exception
{
	public InexistantPropertyException() { }
	public InexistantPropertyException(string message) : base(message) { }
}