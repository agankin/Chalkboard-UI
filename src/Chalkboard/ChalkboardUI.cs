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

        Render();
    }

    private void OnRenderRequired(object? _, RenderRequiredEventArgs args)
    {
        Render();
    }

    private void Render()
    {
        if (_root == null)
            throw new InvalidOperationException($"{nameof(_root)} is null.");

        var rect = new RenderingRect(_renderingSize);

        _root.Render(rect);
        _renderer.Render(rect);
    }
}