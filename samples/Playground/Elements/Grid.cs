using Chalkboard;

namespace Playground;

public class Grid : Element<SquareStore>
{
    public Grid(Store<SquareStore> store) : base(store)
    {
        Children.AddRange(CreateSquares(store));
    }

    public override RenderedRect Render()
    {
        var totalWidth = (ushort)(Square.Length * Store.Cols);
        var totalHeight = (ushort)(Square.Length * Store.Rows);
        var renderedRect = RenderedRect.CreateFilled(totalWidth, totalHeight, ' ');

        foreach (Square square in Children)
        {
            var left = (ushort)(Square.Length * square.Left);
            var top = (ushort)(Square.Length * square.Top);
            
            var squareRenderedRect = square.Render();
            renderedRect = renderedRect.ApplyRendered(left, top, squareRenderedRect);
        }

        return renderedRect;
    }

    private static IEnumerable<Square> CreateSquares(Store<SquareStore> store)
    {
        var storeValue = store.Value;
        for (var left = 0; left < storeValue.Cols; left++)
            for (var top = 0; top < store.Value.Rows; top++)
                yield return new Square(store, left, top);
    }
}