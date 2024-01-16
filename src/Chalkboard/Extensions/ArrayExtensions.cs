using Chalkboard;

public static class ArrayExtensions
{
    public static ushort GetWidth<TValue>(this TValue[,] array) => checked((ushort)array.GetLength(0));

    public static ushort GetHeight<TValue>(this TValue[,] array) => checked((ushort)array.GetLength(1));

    public static IEnumerable<Positioned<TValue>> EnumeratePositioned<TValue>(this TValue[,] array)
    {
        var width = array.GetWidth();
        var height = array.GetHeight();

        foreach (ushort left in Enumerable.Range(0, width))
            foreach (ushort top in Enumerable.Range(0, height))
            {
                var value = array[left, top];
                var position = new Point(left, top);
                
                yield return new(value, position);
            }
    }
}