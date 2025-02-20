namespace Chalkboard.Samples;

public class KeyboardListener : IDisposable
{
    private const int SleepInterval = 10;

    private readonly CancellationTokenSource _cancellationTokenSource;

    public KeyboardListener(CancellationTokenSource cancellationTokenSource)
    {
        _cancellationTokenSource = cancellationTokenSource;
    }

    public static KeyboardListener Start(Action<KeyPressedMessage> onKeyPressed)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var thread = new Thread(CreateKeyListener(onKeyPressed, cancellationTokenSource.Token));
        thread.Start();

        return new KeyboardListener(cancellationTokenSource);
    }

    public void Dispose() => _cancellationTokenSource.Cancel();

    private static ThreadStart CreateKeyListener(Action<KeyPressedMessage> onKeyPressed, CancellationToken cancellationToken)
    {
        return () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(SleepInterval);
                if (!Console.KeyAvailable)
                    continue;

                var key = Console.ReadKey(true).Key;
                onKeyPressed(new KeyPressedMessage(key));
            }
        };
    }
}