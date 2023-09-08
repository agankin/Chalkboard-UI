namespace Chalkboard;

public readonly record struct Symbol(
    char Value,
    Color Foreground,
    Color Background
);