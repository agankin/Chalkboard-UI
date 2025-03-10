namespace Chalkboard;

public class ContentElement<TStore> : Element<TStore>
{
    public ContentElement()
    {
        AutoUpdateStoreUpdatable(() => Content);
    }

    public ComputedValue<TStore, Element<TStore>> Content { get; set; } = null!;

    public override RenderedRect Render(Size size)
    {
        if (Content?.Value == null)
            return new RenderedRect(0, 0);

        return Content.Value.Render(size);
    }
}