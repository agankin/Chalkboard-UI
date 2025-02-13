namespace Chalkboard;

public interface IRenderer
{
    public event Action SizeChanged;

    Size Size { get; }

    void Render(RenderedRect rect);
}