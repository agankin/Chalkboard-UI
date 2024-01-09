using Chalkboard;

namespace Playgrournd;

public class Grid : Element
{
    private readonly Square[,] _squares;

    public Grid(Square[,] squares)
    {
        _squares = squares;

        var flattenSquares = Flatten(squares).Select(sqIdx => sqIdx.Square);
        Children.AddRange(flattenSquares);
    }

    protected override void RenderTo(RenderingRect rect)
    {
        var totalWidth = (ushort)(Square.Length * _squares.GetLength(0));
        var totalHeight = (ushort)(Square.Length * _squares.GetLength(1));
        rect.Fill(new(0, 0), new(totalWidth, totalHeight), new Symbol(' ', Color.White, Color.White));

        foreach (var squareIdx in Flatten(_squares))
        {
            var (square, leftIdx, topIdx) = squareIdx;

            var left = (ushort)(Square.Length * leftIdx);
            var top = (ushort)(Square.Length * topIdx);

            var start = new Point(left, top);
            var size = new Size(Square.Length, Square.Length);
            
            var squareRect = rect.Slice(start, size);
            square.Render(squareRect);
        }
    }

    private static IEnumerable<SquareIdx> Flatten(Square[,] squares)
    {
        for (var leftIdx = 0; leftIdx < squares.GetLength(0); leftIdx++)
            for (var topIdx = 0; topIdx < squares.GetLength(1); topIdx++)
            {
                var square = squares[leftIdx, topIdx];
                yield return new SquareIdx(square, leftIdx, topIdx);
            }
    }

    private readonly record struct SquareIdx(
        Square Square,
        int LeftIdx,
        int TopIdx
    );
}