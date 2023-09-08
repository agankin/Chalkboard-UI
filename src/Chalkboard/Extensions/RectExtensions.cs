namespace Chalkboard;

public static class RectExtensions
{
    public static Rect Slice(this Rect rect, Point relativeStart, Size size)
    {
        var (left, top) = rect.GetAbsolutePoint(relativeStart);
        
        if (!rect.Contains(left, top))
            throw new IndexOutOfRangeException();

        var (width, height) = size;
        var sliceRect = new Rect(left, top, width, height);

        return sliceRect;
    }

    public static Point GetAbsolutePoint(this Rect rect, Point relativePoint)
    {
        var (relativeLeft, relativeTop) = relativePoint;
        return rect.GetAbsolutePoint(relativeLeft, relativeTop);
    }

    public static Point GetAbsolutePoint(this Rect rect, ushort relativeLeft, ushort relativeTop)
    {
        var left = (ushort)(rect.Left + relativeLeft);
        var top = (ushort)(rect.Top + relativeTop);

        return new Point(left, top);
    }

    public static bool Contains(this Rect rect, ushort left, ushort top)
    {
        if (rect.Left > left || rect.Left + rect.Width <= left)
            return false;

        if (rect.Top > top || rect.Top + rect.Height <= top)
            return false;

        return true;
    }
}