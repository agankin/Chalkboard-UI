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

    public override RenderedRect Render()
    {
        var totalWidth = (ushort)(Square.Length * _squares.GetWidth());
        var totalHeight = (ushort)(Square.Length * _squares.GetHeight());
        var renderedRect = RenderedRect.CreateFilled(totalWidth, totalHeight, ' ');

        foreach (var squareIdx in Flatten(_squares))
        {
            var (square, leftIdx, topIdx) = squareIdx;

            var left = (ushort)(Square.Length * leftIdx);
            var top = (ushort)(Square.Length * topIdx);
            
            var squareRenderedRect = square.Render();
            renderedRect = renderedRect.ApplyRendered(left, top, squareRenderedRect);
        }

        return renderedRect;
    }

    private static IEnumerable<SquareIdx> Flatten(Square[,] squares)
    {
        for (var leftIdx = 0; leftIdx < squares.GetWidth(); leftIdx++)
            for (var topIdx = 0; topIdx < squares.GetHeight(); topIdx++)
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