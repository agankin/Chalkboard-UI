namespace Chalkboard;

public class WindowsConsoleRenderer : IRenderer
{
    private readonly WindowsConsoleSizeObserver _consoleSizeObserver;    
    private int _width;
    private int _height;
    private RenderedRect? _renderedRect;

    public WindowsConsoleRenderer(int width, int height)
    {
        _consoleSizeObserver = new WindowsConsoleSizeObserver();
        
        _width = width;
        _height = height;

        WindowsConsoleConfigurer.Run();
    }

    public event Action? SizeChanged;

    public Size GetRenderingSize() => new(_width, _height);

    public void Render(RenderedRect rect)
    {
        var newRenderedRect = new RenderedRect(_width, _height)
            .Fill(Symbol.Default)
            .Apply(rect);
        
        var sizeChanged = CheckSizeChanged();
        foreach (var positionedSymbol in newRenderedRect)
        {
            var (position, newSymbol) = positionedSymbol;
            var (left, top) = position;
            
            var writeSymbol = sizeChanged || _renderedRect == null || !newSymbol.Equals(_renderedRect[left, top]);
            if (writeSymbol)
            {
                WriteSymbol(left, top, newSymbol);
            }
        }
        
        Console.SetCursorPosition(0, 0);

        _renderedRect = newRenderedRect;
    }

    private static void WriteSymbol(int left, int top, Symbol symbol)
    {
        try
        {
            Console.SetCursorPosition(left, top);
            
            Console.ForegroundColor = symbol.Foreground.ToConsoleColor();
            Console.BackgroundColor = symbol.Background.ToConsoleColor();
            Console.Write(symbol.Value);
        }
        catch {}
    }

    private bool CheckSizeChanged() => _renderedRect != null
        && (_width != _renderedRect.Width || _height != _renderedRect.Height);
}