using System.Collections;
using System.Collections.Immutable;

namespace Chalkboard;

public class RenderedRect : IEnumerable<PositionedSymbol>
{
    private readonly ImmutableDictionary<Point, Symbol> _symbolByPosition;

    public RenderedRect(ushort width, ushort height, IEnumerable<PositionedSymbol> symbols)
    {
        Width = width;
        Height = height;

        _symbolByPosition = symbols.Aggregate(ImmutableDictionary<Point, Symbol>.Empty, AddEntry);
    }

    public RenderedRect(ushort width, ushort height, ImmutableDictionary<Point, Symbol> symbolByPosition)
    {
        Width = width;
        Height = height;

        _symbolByPosition = symbolByPosition;
    }

    public ushort Width { get; }

    public ushort Height { get; }

    public Option<Symbol> this[Point point]
    {
        get
        {
            var (left, top) = point;

            return _symbolByPosition.TryGetValue(new(left, top), out var symbol)
                ? Option.Some(symbol)
                : Option.None<Symbol>();
        }
    }

    public static RenderedRect CreateFilled(ushort width, ushort height, Symbol symbol)
    {
        var leftRange = Enumerable.Range(0, width).Select(val => (ushort)val);
        var topRange = Enumerable.Range(0, height).Select(val => (ushort)val);

        var renderedPoints = topRange.SelectMany(top => leftRange.Select(left => new Point(left, top)))
            .Select(point => new PositionedSymbol(point, symbol));
        var symbolByPosition = renderedPoints.Aggregate(ImmutableDictionary<Point, Symbol>.Empty, AddEntry);

        return new(width, height, symbolByPosition);
    }

    public IEnumerator<PositionedSymbol> GetEnumerator() => _symbolByPosition.Select(ToPositionedSymbol).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public RenderedRect ApplyRendered(ushort left, ushort top, RenderedRect innerRect)
    {
        var symbolByPosition = innerRect.Select(Translate(left, top)).Aggregate(_symbolByPosition, AddEntry);

        return new(Width, Height, symbolByPosition);
    }

    private static ImmutableDictionary<Point, Symbol> AddEntry(ImmutableDictionary<Point, Symbol> dictionary, PositionedSymbol positionedSymbol) =>
        dictionary.SetItem(positionedSymbol.Position, positionedSymbol.Symbol);

    private static PositionedSymbol ToPositionedSymbol(KeyValuePair<Point, Symbol> pointSymbol)
    {
        var (point, symbol) = pointSymbol;

        return new(point, symbol);
    }

    private static Func<PositionedSymbol, PositionedSymbol> Translate(ushort left, ushort top) =>
        positionedSymbol => positionedSymbol with { Position = positionedSymbol.Position.Translate(left, top) };
}