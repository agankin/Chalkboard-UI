namespace Chalkboard;

public abstract class Element<TStore>
{
    private readonly Store<TStore> _store;

    protected Element(Store<TStore> store)
    {
        _store = store;
        Children = new ChildCollection<TStore>(this);
    }

    protected TStore Store => _store.Value;

    public Element<TStore>? Parent { get; internal set; }

    public ChildCollection<TStore> Children { get; }

    public Option<Size> Size { get; }
    
    public Option<Margin> Margin { get; set; }

    public abstract RenderedRect Render();

    protected virtual void OnStoreUpdated() {}

    internal void StoreUpdated()
    {
        OnStoreUpdated();
        Children.NotifyStoreUpdated();
    }
}