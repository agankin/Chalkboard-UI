using Chalkboard;

namespace Playgrournd;

public class Square : Element
{   
    public const int Length = 6;
    
    private Option<Color> _background;

    public Square()
    {
    }

    public Option<Color> Background
    {
        get => _background;
        set => SetRenderableProperty(ref _background, value);
    }

    public override RenderedRect Render()
    {
        var colorScheme = ColorScheme.Default with { Background = Background.ValueOr(Color.White) };
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