using System.Collections;

namespace Chalkboard;

public class RenderedRect : IEnumerable<PositionedSymbol>
{
    private readonly Margin _margin = new Margin(0, 0);
    private readonly Dictionary<Position, Symbol> _symbolByPosition = new();

    public RenderedRect(int width, int height) => (Width, Height) = (width, height);

    public RenderedRect(Size size) => (Width, Height) = size;

    private RenderedRect(Dictionary<Position, Symbol> symbolByPosition, int width, int height, Margin margin)
        : this(width, height)
    {
        _symbolByPosition = symbolByPosition;
        _margin = margin;
    }

    public int Width { get; }
    
    public int Height { get; }

    public Symbol this[int left, int top]
    {
        get
        {
            var position = new Position(left, top).Translate(_margin);
            return _symbolByPosition.TryGetValue(position, out Symbol symbol) ? symbol : Symbol.Default;
        }
        set
        {
            var position = new Position(left, top).Translate(_margin);
            var rect = new Rect(_margin.Left, _margin.Top, Width, Height);
            
            if (!rect.Contains(position.Left, position.Top))
                return;

            _symbolByPosition[position] = value;
        }
    }

    public RenderedRect GetSubRect(Thickness padding)
    {
        var width = Width - padding.Left - padding.Right;
        var height = Height - padding.Top - padding.Bottom;
        var margin = new Margin(padding.Left, padding.Top);
        
        return new RenderedRect(_symbolByPosition, width, height, margin);
    }

    public IEnumerator<PositionedSymbol> GetEnumerator() => _symbolByPosition.Select(AsPositionedSymbol).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static PositionedSymbol AsPositionedSymbol(KeyValuePair<Position, Symbol> positionSymbol)
    {
        var (position, symbol) = positionSymbol;
        var (left, top) = position;

        return new(left, top, symbol);
    }
}