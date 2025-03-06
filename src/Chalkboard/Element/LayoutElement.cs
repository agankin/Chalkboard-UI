namespace Chalkboard;

public abstract class LayoutElement<TStore> : Element<TStore>
{
    protected LayoutElement()
    {
        Children = new();
        AutoUpdateStoreUpdatable(() => Children);
    }

    public StoreUpdatableCollection<TStore, Element<TStore>> Children { get; }

    protected override void OnStoreUpdated()
    {
        base.OnStoreUpdated();
        
        Children.Clear();
        var children = GetChildren();
        Children.AddRange(children);
    }

    protected abstract IEnumerable<Element<TStore>> GetChildren();
}