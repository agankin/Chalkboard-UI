using Chalkboard;

public class ChalkboardUI
{
    private readonly IRenderer _renderer;
    private readonly Size _renderingSize;

    private Element? _root;

    public ChalkboardUI(IRenderer renderer, Size renderingSize)
    {
        _renderer = renderer;
        _renderingSize = renderingSize;
    }

    public void Render(Element root)
    {
        if (_root != null)
            _root.RenderRequired -= OnRenderRequired;

        _root = root;
        _root.RenderRequired += OnRenderRequired;
    }

    private void OnRenderRequired(object? _, RenderRequiredEventArgs args)
    {
        if (_root == null)
            return;

        var renderingRect = new RenderingRect(_renderingSize);
        
        _root.Render(renderingRect);
        _renderer.Render(renderingRect);
    }
}