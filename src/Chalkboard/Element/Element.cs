using System.Linq.Expressions;
using System.Reflection;

namespace Chalkboard;

public abstract class Element<TStore> : IStoreUpdatable<TStore>
{
    private readonly List<Func<IStoreUpdatable<TStore>>> _getStoreUpdatableList = new();
    
    protected Element()
    {
    }

    public TStore Store { get; private set; } = default!;

    public Element<TStore>? Parent { get; internal set; }

    public ComputedValue<TStore, Margin> Margin { get; } = new Margin(0, 0);

    public virtual void UpdateStore(TStore store)
    {
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