namespace Chalkboard;

public interface IRenderer
{
    public event Action SizeChanged;

    Size GetRenderingSize();
    
    void Render(RenderedRect rect);
}