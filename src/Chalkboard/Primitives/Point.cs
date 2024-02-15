namespace Chalkboard;

public readonly record struct Point(
    ushort Left,
    ushort Top
)
{
    public Point Translate(ushort left, ushort top) => this with
    {
        Left = (ushort)(Left + left),
        Top = (ushort)(Top + top)
    };
}