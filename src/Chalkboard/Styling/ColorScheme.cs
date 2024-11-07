namespace Chalkboard;

public record ColorScheme
{
    private const Color DefaultForeground = Color.White;
    
    private const Color DefaultBackground = Color.Black;

    static ColorScheme()
    {
        Current = Default;
    }

    public Color Foreground { get; private set; }

    public Color Background { get; private set; }

    public static ColorScheme Current { get; internal set; }

    public static ColorScheme Default { get; } = new()
    {
        Foreground = DefaultForeground,
        Background = DefaultBackground
    };

    public ColorScheme UseForeground(Color foreground) => this with { Foreground = foreground };

    public ColorScheme UseBackground(Color background) => this with { Background = background };

    public ColorSchemeScope CreateScope() => new(this);

    public static void SetDefault(Color foreground, Color background)
    {
        Default.Foreground = foreground;
        Default.Background = background;
    }

    public void Deconstruct(out Color foreground, out Color background)
    {
        foreground = Foreground;
        background = Background;
    }
}