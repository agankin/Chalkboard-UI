namespace Chalkboard;

public record PositionedSymbol(
    Position Position,
    Symbol Symbol
)
{
    public PositionedSymbol(int left, int top, Symbol symbol)
        : this(new Position(left, top), symbol)
    {
    }
}