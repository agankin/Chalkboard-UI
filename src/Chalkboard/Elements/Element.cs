namespace Chalkboard;

public abstract class Element<TStore> : IStoreUpdatable<TStore>
{
    private readonly List<Func<IStoreUpdatable<TStore>>> _getStoreUpdatableList = new();
    
    protected Element()
    {
    }

    public TStore Store { get; private set; } = default!;

    public ComputedValue<TStore, Margin> Margin { get; set; } = new Margin(0, 0);

    public virtual void UpdateStore(TStore store)
    {
        if (EqualityComparer<TStore>.Default.Equals(Store, store))
            return;

        Store = store;

        OnStoreUpdated();
        _getStoreUpdatableList.ForEach(getter => getter()?.UpdateStore(Store));
    }

    public abstract RenderedRect Render(Size size);

    protected virtual void OnStoreUpdated()
    {
    }

    protected void AutoUpdateStoreUpdatable(Func<IStoreUpdatable<TStore>> getStoreUpdatable)
    {
        _getStoreUpdatableList.Add(getStoreUpdatable);
    }
}