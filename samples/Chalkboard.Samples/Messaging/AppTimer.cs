namespace Chalkboard.Samples;

public class AppTimer : IDisposable
{
    private readonly Timer _timer;

    public AppTimer(Timer timer)
    {
        _timer = timer;
    }

    public static AppTimer Start(Action<TickMessage> onTick, int tickMilliseconds)
    {
        var timer = new Timer(
            callback: _ => onTick(new()),
            state: null,
            dueTime: 0,
            period: tickMilliseconds);

        return new AppTimer(timer);
    }

    private static void OnTick(object? state)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}