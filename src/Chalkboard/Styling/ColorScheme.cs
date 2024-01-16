namespace Chalkboard;

public readonly record struct ColorScheme(
    Color Foreground,
    Color Background
)
{
    public static readonly ColorScheme Default = new(
        Foreground: DefaultColors.Foreground,
        Background: DefaultColors.Background
    );
}