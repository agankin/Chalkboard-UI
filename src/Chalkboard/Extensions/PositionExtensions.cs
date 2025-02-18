namespace Chalkboard;

public static class PositionExtensions
{
    public static Position Translate(this in Position position, Margin margin)
    {
        var (leftOffset, topOffset) = margin;

        return position.Translate(leftOffset, topOffset);
    }

    public static Position TranslateLeft(this in Position position, int leftOffset)
    {
        return position.Translate(leftOffset: leftOffset, topOffset: 0);
    }

    public static Position TranslateTop(this in Position position, int topOffset)
    {
        return position.Translate(leftOffset: 0, topOffset: topOffset);
    }
}