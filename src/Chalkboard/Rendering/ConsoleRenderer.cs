namespace Chalkboard;

public class ConsoleRenderer : IRenderer
{
    private readonly Symbol[,] _symbols;

    public ConsoleRenderer(ushort width, ushort height)
    {
        _symbols = new Symbol[width, height];
    }

    public void Render(RenderingRect rect)
    {
        var leftRange = Enumerable.Range(0, rect.Width).Cast<ushort>();
        var topRange = Enumerable.Range(0, rect.Height).Cast<ushort>();

        var renderedPoints = topRange.SelectMany(top => leftRange.Select(left => new Point(left, top)));
        
        foreach (var point in renderedPoints)
        {
            var (left, top) = point;
            var currentSymbol = _symbols[left, top];
            var renderedSymbol = rect[left, top];

            if (renderedSymbol != currentSymbol)
            {
                _symbols[left, top] = renderedSymbol;
                Render(renderedSymbol, left, top);
            }
        }
    }

    private static void Render(Symbol symbol, ushort left, ushort top)
    {
        Console.SetCursorPosition(left, top);
        
        Console.ForegroundColor = symbol.Foreground.ToConsoleColor();
        Console.BackgroundColor = symbol.Background.ToConsoleColor();
        Console.Write(symbol.Value);
    }
}