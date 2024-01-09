namespace Chalkboard;

public readonly record struct Symbol(
    char Value,
    Color Foreground,
    Color Background
)
{
    private const Color DefaultForeground = Color.White;
    private const Color DefaultBackground = Color.Black;

    public static implicit operator Symbol(char symbol) =>
        new(symbol, Foreground: DefaultForeground, Background: DefaultBackground);
}