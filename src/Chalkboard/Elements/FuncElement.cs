namespace Chalkboard;

public class FuncElement<TStore> : Element<TStore>
{
    private readonly Func<TStore, Size, RenderedRect> _render;

    public FuncElement(Func<TStore, Size, RenderedRect> render)
    {
        _render = render;
    }

    public override RenderedRect Render(Size size) => _render(Store, size);
}