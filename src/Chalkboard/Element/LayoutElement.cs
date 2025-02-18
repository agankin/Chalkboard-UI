namespace Chalkboard;

public abstract class LayoutElement<TStore> : Element<TStore>
{
    protected LayoutElement()
    {
        Children = new(this);

        AutoUpdateStoreUpdatable(() => Children);
    }

    public ChildCollection<TStore> Children { get; protected set; }

    protected abstract IEnumerable<Element<TStore>> GetChildren();

    protected override void OnStoreUpdated()
    {
        base.OnStoreUpdated();
        
        Children.Clear();
        var children = GetChildren();
        Children.AddRange(children);
    }
}