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

    protected override void RenderTo(RenderingRect rect)
    {
        ColorSchemeContext.CreateFor(scheme => scheme with { Background = Background.ValueOr(Color.White) })
            .DoInContext(() => RenderCore(rect));
    }

    private void RenderCore(RenderingRect rect)
    {
        foreach (ushort left in Enumerable.Range(0, Length))
            foreach (ushort top in Enumerable.Range(0, Length))
                rect[left, top] = ' ';
    }
}