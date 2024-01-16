namespace Chalkboard;

public readonly record struct Positioned<TValue>(
    TValue Value,
    Point Position
);