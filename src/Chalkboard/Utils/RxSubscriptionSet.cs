using System.Collections;

namespace Chalkboard;

internal class RxSubscriptionSet<TValue> : IEnumerable<RxSubscription<TValue>>
{
    private readonly ConcurrentSet<RxSubscription<TValue>> _subscriptions = new();

    internal RxSubscription<TValue> Add(IObserver<TValue> observer)
    {
        var subscription = new RxSubscription<TValue>(observer, this);
        _subscriptions.Add(subscription);
        
        return subscription;
    }

    internal void Remove(RxSubscription<TValue> subscription) => _subscriptions.Remove(subscription);
    
    /// <inheritdoc/>
    public IEnumerator<RxSubscription<TValue>> GetEnumerator() => _subscriptions.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}