namespace Chalkboard;

public readonly record struct Symbol(
    char Value,
    Color Foreground,
    Color Background
)
{
    public static implicit operator Symbol(char symbol)
    {
        var (foreground, background) = ColorSchemeContext.CurrentScheme;
        return new(symbol, foreground, background);
    }
}