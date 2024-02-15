public static class ArrayExtensions
{
    public static ushort GetWidth<TValue>(this TValue[,] array) => checked((ushort)array.GetLength(0));

    public static ushort GetHeight<TValue>(this TValue[,] array) => checked((ushort)array.GetLength(1));
}