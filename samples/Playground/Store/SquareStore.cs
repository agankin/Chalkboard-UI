using Chalkboard;

namespace Playground;

public class SquareStore
{
    private SquareViewModel[,] _squares;

    public SquareStore(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        
        _squares = new SquareViewModel[cols, rows];
    }

    public int Rows { get; }

    public int Cols { get; }

    public SquareViewModel this[int left, int top] => _squares[left, top];

    public SquareStore SetBackground(int left, int top, Color background)
    {
        _squares[left, top] = new() { Background = background };
        return this;
    }
}