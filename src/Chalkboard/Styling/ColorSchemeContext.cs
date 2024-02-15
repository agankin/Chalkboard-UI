namespace Chalkboard;

public class ColorSchemeContext
{
    private readonly ColorScheme _colorScheme;

    private ColorSchemeContext(ColorScheme colorScheme) => _colorScheme = colorScheme;

    public static ColorScheme CurrentScheme { get; private set; } = ColorScheme.Default;

    public static ColorSchemeContext CreateFor(Func<ColorScheme, ColorScheme> configureScheme) =>
        new(configureScheme(ColorScheme.Default));

    public TResult DoInContext<TResult>(Func<TResult> func)
    {
        try
        {
            CurrentScheme = _colorScheme;
            return func();
        }
        finally
        {
            CurrentScheme = ColorScheme.Default;
        }
    }
}