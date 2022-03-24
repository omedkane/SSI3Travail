namespace DatabaseSimulator.Exceptions;

public class PrimaryKeyException : Exception
{
    public PrimaryKeyException() { }

    public PrimaryKeyException(string message) : base(message) { }
}

public class InexistantTableException : Exception
{
    public InexistantTableException() { }

    public InexistantTableException(string message) : base(message) { }
}
