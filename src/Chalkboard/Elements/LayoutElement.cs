namespace Chalkboard;

public class LayoutElement<TStore> : Element<TStore>
{
    protected LayoutElement()
    {
        Children = new();
        AutoUpdateStoreUpdatable(() => Children);
    }

    public StoreUpdatableCollection<TStore, Element<TStore>> Children { get; }

    public override RenderedRect Render(Size size)
    {
        var renderedRect = new RenderedRect(size);
        Children.ForEach(child => child.Render(size));

        return renderedRect;
    }

    protected override void OnStoreUpdated()
    {
        base.OnStoreUpdated();
        
        var children = GetChildren();
        if (children == Children)
            return;

        Children.Clear();
        Children.AddRange(children);
    }

    protected virtual IEnumerable<Element<TStore>> GetChildren() => Children;
}