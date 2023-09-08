namespace Chalkboard;

public record SymbolArray(ushort Width, ushort Height)
{
    private readonly Symbol[,] _array = new Symbol[Width, Height];

    public Symbol this[ushort left, ushort top]
    {
        get
        {
            EnsureInRange(left, top);
            return _array[left, top];
        }
        set
        {
            EnsureInRange(left, top);
            _array[left, top] = value;
        }
    }

    private void EnsureInRange(ushort left, ushort top)
    {
        if (left >= Width)
            throw new IndexOutOfRangeException(nameof(left));

        if (top >= Height)
            throw new IndexOutOfRangeException(nameof(top));
    }
}