namespace Chalkboard;

public readonly record struct ColorScheme(
    Color Foreground,
    Color Background
)
{
    public static ColorScheme Current { get; private set; }

    public static readonly ColorScheme Default = new(
        Foreground: DefaultColors.Foreground,
        Background: DefaultColors.Background
    );

    public TResult DoInScope<TResult>(Func<TResult> func)
    {
        var previousScheme = Current;

        try
        {
            Current = this;
            return func();
        }
        finally
        {
            Current = previousScheme;
        }
    }
}