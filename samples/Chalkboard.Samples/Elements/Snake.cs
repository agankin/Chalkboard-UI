namespace Chalkboard.Samples;

public class Snake : Element<AppStore>
{
    public override RenderedRect Render(Size size)
    {
        var initialRect = new RenderedRect(size);
        return Store.Snake
            .Select(IndexedPosition.Create)
            .Aggregate(initialRect, AddPosition);
    }

    private static RenderedRect AddPosition(RenderedRect rect, IndexedPosition indexedPosition)
    {
        var (index, position) = indexedPosition;
        var symbol = index switch
        {
            0 => 'H',
            int idx when idx % 2 == 0 => 'Y',
            _ => 'X'
        };

        return rect.Apply(new PositionedSymbol(position, symbol));
    }

    private record IndexedPosition(int Index, Position Position)
    {
        public static IndexedPosition Create(Position position, int idx) => new(idx, position);
    }
}