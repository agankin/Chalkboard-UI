namespace Chalkboard;

public class SymbolRect
{
    private readonly SymbolArray _screenSymbols;
    private readonly Rect _screenRect;

    public SymbolRect(SymbolArray screenSymbols)
    {
        _screenSymbols = screenSymbols;

        var width = screenSymbols.Width;
        var height = screenSymbols.Height;
        _screenRect = new Rect(0, 0, width, height);
    }

    private SymbolRect(SymbolArray screenSymbols, Rect screenRect)
    {
        _screenSymbols = screenSymbols;
        _screenRect = screenRect;
    }

    public ushort Width => _screenRect.Width;

    public ushort Height => _screenRect.Height;
    
    public Symbol this[ushort relativeLeft, ushort relativeTop]
    {
        get
        {
            var relativePoint = new Point(relativeLeft, relativeTop);
            var screenPoint = _screenRect.GetAbsolutePoint(relativePoint);

            return _screenSymbols[screenPoint.Left, screenPoint.Top];
        }
        set
        {
            var relativePoint = new Point(relativeLeft, relativeTop);
            var screenPoint = _screenRect.GetAbsolutePoint(relativePoint);

            _screenSymbols[screenPoint.Left, screenPoint.Top] = value;
        }
    } 

    public SymbolRect Slice(Point relativeStart, Size size)
    {
        var screenStart = _screenRect.GetAbsolutePoint(relativeStart);
        var sliceScreenRect = _screenRect.Slice(screenStart, size);

        return new SymbolRect(_screenSymbols, sliceScreenRect);
    }
}