namespace Chalkboard.Samples;

public class TimerObservable : IObservable<TimerEvent>, IDisposable
{
    private readonly RxSubscriptionSet<TimerEvent> _subscriptions = new();
    private readonly Timer _timer;
    
    private volatile int _disposed = 0;

    private TimerObservable(Func<Action, Timer> createTimer)
    {
        _timer = createTimer(OnTick);
    }

    public static TimerObservable Start(int tickMilliseconds, int dueTimeMilliseconds = 0)
    {
        Func<Action, Timer> createTimer = onTick => new Timer(
            callback: _ => onTick(),
            state: null,
            dueTime: dueTimeMilliseconds,
            period: tickMilliseconds);

        var instance = new TimerObservable(createTimer);
        return instance;
    }

    public IDisposable Subscribe(IObserver<TimerEvent> observer) => _subscriptions.Add(observer);

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        _timer.Dispose();
        _subscriptions.ForEach(subscription => subscription.Dispose());
    }

    private void OnTick() => _subscriptions.ForEach(subscription => subscription.OnNext(new()));
}