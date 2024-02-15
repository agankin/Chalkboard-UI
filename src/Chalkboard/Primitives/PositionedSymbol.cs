namespace Chalkboard;

public readonly record struct PositionedSymbol(
    Point Position,
    Symbol Symbol
);