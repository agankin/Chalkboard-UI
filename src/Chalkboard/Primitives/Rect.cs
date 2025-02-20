namespace Chalkboard;

public record Rect(
    int Left,
    int Top,
    int Width,
    int Height
)
{
    public static Rect WithSize(int width, int height) => new(Left: 0, Top: 0, Width: width, Height: height);
    
    public static Rect WithSize(Size size)
    {
        var (width, height) = size;
        return WithSize(width, height);
    }

    public bool Contains(int left, int top)
    {
        return left >= Left && left < Left + Width
            && top >= Top && top < Top + Height;
    }
}