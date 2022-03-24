namespace DatabaseSimulator;

public class MutationResult<F, N>
{
    public List<F> Successful = new List<F>();
    public List<N> Failed = new List<N>();

    public MutationResult() { }

    public void AddSuccess(F success) => Successful.Add(success);

    public void AddFailure(N failure) => Failed.Add(failure);

    public bool HasSucceeded() => Successful.Count > 0 && Failed.Count == 0;

    public bool HasFailed() => Failed.Count > 0 && Successful.Count == 0;
}
