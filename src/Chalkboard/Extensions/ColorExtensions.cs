namespace Chalkboard;

public static class ColorExtensions
{
    public static ConsoleColor ToConsoleColor(this Color color) => color switch
    {
        Color.Black => ConsoleColor.Black,
        Color.DarkBlue => ConsoleColor.DarkBlue,
        Color.DarkGreen => ConsoleColor.DarkGreen,
        Color.DarkCyan => ConsoleColor.DarkCyan,
        Color.DarkRed => ConsoleColor.DarkRed,
        Color.DarkMagenta => ConsoleColor.DarkMagenta,
        Color.DarkYellow => ConsoleColor.DarkYellow,
        Color.Gray => ConsoleColor.Gray,
        Color.DarkGray => ConsoleColor.DarkGray,
        Color.Blue => ConsoleColor.Blue,
        Color.Green => ConsoleColor.Green,
        Color.Cyan => ConsoleColor.Cyan,
        Color.Red => ConsoleColor.Red,
        Color.Magenta => ConsoleColor.Magenta,
        Color.Yellow => ConsoleColor.Yellow,
        Color.White => ConsoleColor.White,
        _ => throw new Exception($"Unsupported {nameof(Color)} enum value: {color}.")
    };
}