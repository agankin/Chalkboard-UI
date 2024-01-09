namespace Chalkboard;

public class RenderingRect
{
    private readonly Rect _rect;
    private readonly Symbol[,] _symbols;

    public RenderingRect(Size size)
    {
        var (width, height) = size;

        _rect = new Rect(Left: 0, Top: 0, Width: width, Height: height);
        _symbols = new Symbol[width, height];
    }

    public ushort Width => _rect.Width;

    public ushort Height => _rect.Height;
    
    private RenderingRect(Rect rect, Symbol[,] symbols)
    {
        _rect = rect;
        _symbols = symbols;
    }
    
    public Symbol this[ushort relativeLeft, ushort relativeTop]
    {
        get
        {
            var relativePoint = new Point(relativeLeft, relativeTop);
            var screenPoint = _rect.GetAbsolutePoint(relativePoint);

            return _symbols[screenPoint.Left, screenPoint.Top];
        }
        set
        {
            var relativePoint = new Point(relativeLeft, relativeTop);
            var screenPoint = _rect.GetAbsolutePoint(relativePoint);

            _symbols[screenPoint.Left, screenPoint.Top] = value;
        }
    } 

    public RenderingRect Slice(Point relativeStart, Size size)
    {
        var start = _rect.GetAbsolutePoint(relativeStart);
        var slicedRect = _rect.Slice(start, size);

        return new RenderingRect(slicedRect, _symbols);
    }

    public void Fill(Point relativeStart, Size size, Symbol symbol)
    {
        var slicedRect = Slice(relativeStart, size);
        foreach (ushort left in Enumerable.Range(0, size.Width))
        {
            foreach (ushort top in Enumerable.Range(0, size.Height))
            {
                slicedRect[left, top] = symbol;
            }
        }
    }
}