using Chalkboard;

namespace Playground;

public class Square : Element<SquareStore>
{   
    public const int Length = 6;

    public Square(Store<SquareStore> store, int left, int top) : base(store)
    {
        Left = left;
        Top = top;
    }

    public int Left { get; }

    public int Top { get; }

    public override RenderedRect Render()
    {
        var viewModel = Store[Left, Top];
        var colorScheme = ColorScheme.Default with { Background = viewModel.Background };
        var colorSchemeContext = ColorSchemeContext.CreateFor(_ => colorScheme);
        
        var renderedRect = colorSchemeContext.DoInContext(RenderCore);
        return renderedRect;
    }

    private RenderedRect RenderCore()
    {
        var positionedSymbols = new List<PositionedSymbol>();

        foreach (ushort left in Enumerable.Range(0, Length))
        {
            foreach (ushort top in Enumerable.Range(0, Length))
            {
                var position = new Point(left, top);
                var positionedSymbol = new PositionedSymbol(position, ' ');

                positionedSymbols.Add(positionedSymbol);
            }
        }

        return new RenderedRect(Length, Length, positionedSymbols);
    }
}