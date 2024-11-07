namespace Chalkboard;

public static class RenderedRectExtensions
{
    public static Size GetSize(this RenderedRect rect) => new Size(rect.Width, rect.Height);
    
    public static RenderedRect Fill(this RenderedRect rect, Symbol symbol)
    {
        for (int left = 0; left < rect.Width; left++)
        {
            for (int top = 0; top < rect.Height; top++)
            {
                rect[left, top] = symbol;
            }
        }

        return rect;
    }

    public static RenderedRect Apply(this RenderedRect rect, PositionedSymbol positionedSymbol)
    {
        var (position, symbol) = positionedSymbol;
        var (left, top) = position;

        rect[left, top] = symbol;
        
        return rect;
    }
    
    public static RenderedRect Apply(this RenderedRect rect, RenderedRect innerRect)
    {        
        foreach (var symbol in innerRect)
            rect.Apply(symbol);

        return rect;
    }
}