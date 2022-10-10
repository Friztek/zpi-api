using ZPI.Core.Abstraction;

namespace ZPI.API.Abstraction;

public abstract class AwaitableResultPresenterBase<TResult>
{
    private readonly TaskCompletionSource<TResult> resultSource = new();

    public Task<TResult> GetResultAsync() => this.resultSource.Task;

    protected void SetResult(TResult result) => this.resultSource.SetResult(result);

    protected void SetException(Exception exception) => this.resultSource.SetException(exception);
}