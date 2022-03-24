namespace DatabaseSimulator;

public class QueryResult<F, N>
{
    public List<F> Found = new List<F>();
    public List<N> NotFound = new List<N>();

    public QueryResult() { }

    public QueryResult(List<F> found, List<N> notFound)
    {
        Found = found;
        NotFound = notFound;
    }

    public void AddFound(F found) => Found.Add(found);

    public void AddNotFound(N notFound) => NotFound.Add(notFound);

    public bool HasFoundAll() => Found.Count > 0 && NotFound.Count == 0;

    public bool HasFoundNone() => NotFound.Count > 0 && Found.Count == 0;
}
