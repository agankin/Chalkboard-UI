namespace Chalkboard;

public readonly record struct Rect(
    int Left,
    int Top,
    int Width,
    int Height
)
{
    public bool Contains(int left, int top)
    {
        return left >= Left && left <= Left + Width
            && top >= Top && top <= Top + Height;
    }
}