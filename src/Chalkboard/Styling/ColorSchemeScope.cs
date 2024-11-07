namespace Chalkboard;

public class ColorSchemeScope : IDisposable
{
    private readonly ColorScheme _previousScheme;

    public ColorSchemeScope(ColorScheme scheme)
    {
        _previousScheme = ColorScheme.Current;
        ColorScheme.Current = scheme;
    }

    public void Dispose() => ColorScheme.Current = _previousScheme;
}