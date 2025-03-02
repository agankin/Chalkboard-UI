namespace Chalkboard;

internal class RxSubscription<TValue> : IDisposable
{
    private readonly IObserver<TValue> _observer;
    private readonly RxSubscriptionSet<TValue> _subscriptions;

    private volatile int _completed;
    private volatile int _disposed;

    public RxSubscription(IObserver<TValue> observer, RxSubscriptionSet<TValue> subscriptions)
    {
        _observer = observer;
        _subscriptions = subscriptions;
    }

    internal void OnNext(TValue value)
    {
        if (_completed != 0 || _disposed != 0)
            return;

        try
        {
            _observer.OnNext(value);
        }
        catch {}
    }

    internal void Complete()
    {
        if (_disposed != 0)
            return;

        if (Interlocked.Exchange(ref _completed, 1) != 0)
            return;

        try
        {
            _observer.OnCompleted();
        }
        catch {}
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Complete();

        if (Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        _subscriptions.Remove(this);
    }
}