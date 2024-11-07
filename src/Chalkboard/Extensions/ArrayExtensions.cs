namespace Chalkboard;

public static class ArrayExtensions
{
    public static TValue[,] Fill<TValue>(this TValue[,] array, TValue value)
    {
        for (var left = 0; left < array.GetWidth(); left++)
            for (var top = 0; top < array.GetHeight(); top++)
                array[left, top] = value;

        return array;
    }

    public static int GetWidth<TValue>(this TValue[,] array) => array.GetLength(0);

    public static int GetHeight<TValue>(this TValue[,] array) => array.GetLength(1);
}