namespace Chalkboard;

public static class RectExtensions
{
    public static bool Contains(this Rect rect, Position position)
    {
        var (left, top) = position;
        return rect.Contains(left, top);
    }
}