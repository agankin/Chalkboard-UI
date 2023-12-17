namespace Chalkboard;

public class RenderRequiredEventArgs : EventArgs
{
    public RenderRequiredEventArgs(Element source) => Source = source;

    public Element Source { get; }
}