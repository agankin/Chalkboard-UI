namespace Chalkboard;

public class KeyPressedObservable : IObservable<ConsoleKeyInfo>, IDisposable
{
    private readonly RxSubscriptionSet<ConsoleKeyInfo> _subscriptions = new();
    private volatile int _disposed = 0;

    public static KeyPressedObservable Start()
    {
        var instance = new KeyPressedObservable();
        
        var thread = new Thread(_ => instance.ListenForKeyAvailable());
        thread.Start();

        return instance;
    }

    public IDisposable Subscribe(IObserver<ConsoleKeyInfo> observer) => _subscriptions.Add(observer);
    
    public void Dispose()
    {
        var disposed = Interlocked.Exchange(ref _disposed, 1);
        if (disposed != 0)
            return;

        _subscriptions.ForEach(subscription => subscription.Dispose());
    }

    private void ListenForKeyAvailable()
    {
        while (_disposed == 0)
        {
            Thread.Sleep(1);

            if (!Console.KeyAvailable)
                continue;

            var keyInfo = Console.ReadKey(true);
            _subscriptions.ForEach(subscription => subscription.OnNext(keyInfo));
        }
    }
}