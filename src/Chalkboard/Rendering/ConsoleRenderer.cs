namespace Chalkboard;

public class ConsoleRenderer : IRenderer
{
    private readonly ushort _width;
    private readonly ushort _height;
    private readonly RenderedRect _lastRootRect;

    public ConsoleRenderer(ushort width, ushort height)
    {
        _width = width;
        _height = height;

        _lastRootRect = RenderedRect.CreateFilled(width, height, ' ');
    }

    public ConsoleRenderer(Size renderingSize) : this(renderingSize.Width, renderingSize.Height)
    {
    }

    public void Render(RenderedRect rect)
    {
        var newRootRect = RenderedRect.CreateFilled(_width, _height, ' ')
            .ApplyRendered(0, 0, rect);
        
        foreach (var positionedSymbol in newRootRect)
        {
            var (point, renderedSymbol) = positionedSymbol;
            var currentSymbol = _lastRootRect[point].ValueOr(' ');

            if (!renderedSymbol.Equals(currentSymbol))
            {
                var (left, top) = point;
                Render(left, top, renderedSymbol);
            }
        }
    }

    private static void Render(ushort left, ushort top, Symbol symbol)
    {
        Console.SetCursorPosition(left, top);
        
        Console.ForegroundColor = symbol.Foreground.ToConsoleColor();
        Console.BackgroundColor = symbol.Background.ToConsoleColor();
        Console.Write(symbol.Value);
    }
}