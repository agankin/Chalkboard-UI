namespace Chalkboard;

public class WindowsConsoleSizeObserver : IDisposable
{
    private const int ObservationIntervalMilliseconds = 50;

    private readonly Timer _timer;

    private int _width;
    private int _height;

    public WindowsConsoleSizeObserver()
    {
        _timer = new Timer(CheckConsoleChanged, null, ObservationIntervalMilliseconds, ObservationIntervalMilliseconds);
    }

    public event Action<Size>? SizeChanged;

    public void Dispose()
    {
        _timer.Dispose();
    }

    private void CheckConsoleChanged(object? _)
    {
        try
        {
            StopObservation();

            var (width, height) = (Console.WindowWidth, Console.WindowHeight);
            if (_width != width || _height != height)
            {
                (_width, _height) = (width, height);
                SizeChanged?.Invoke(new Size(_width, _height));
            }
        }
        finally
        {
            RunObservation();
        }
    }

    private void StopObservation()
    {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    private void RunObservation()
    {
        _timer.Change(ObservationIntervalMilliseconds, ObservationIntervalMilliseconds);
    }
}