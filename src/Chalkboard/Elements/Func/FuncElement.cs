namespace Chalkboard;

public class FuncElement<TStore> : Element<TStore>
{
    private readonly RenderFunc<TStore> _render;

    public FuncElement(RenderFunc<TStore> render) => _render = render;

    public override RenderedRect Render(Size size) => _render(Store, size);
}