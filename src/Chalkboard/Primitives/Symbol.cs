namespace Chalkboard;

public readonly record struct Symbol(
    char Value,
    Color Foreground,
    Color Background
)
{
    public static Symbol Default => new Symbol(' ', ColorScheme.Default.Foreground, ColorScheme.Default.Background);
    
    public static implicit operator Symbol(char symbol)
    {
        var (foreground, background) = ColorScheme.Current;
        return new(symbol, foreground, background);
    }

    public static implicit operator Symbol(int symbol)
    {
        var (foreground, background) = ColorScheme.Current;
        return new((char)symbol, foreground, background);
    }
}