namespace Chalkboard;

public readonly record struct Position(
    int Left,
    int Top
)
{
    public Position Translate(int leftOffset, int topOffset) => this with
    {
        Left = Left + leftOffset,
        Top = Top + topOffset
    };

    public Position Translate(Margin margin)
    {
        var (leftOffset, topOffset) = margin;

        return this with
        {
            Left = Left + leftOffset,
            Top = Top + topOffset
        };
    }
}