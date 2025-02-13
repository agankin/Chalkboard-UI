using System.Reflection;

namespace Chalkboard;

public abstract class Element<TStore> : IStoreUpdatable<TStore>
{
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
        UpdateStoreUpdatables();
    }

    public abstract RenderedRect Render(Size size);

    protected virtual void OnStoreUpdated()
    {
    }

    private void UpdateStoreUpdatables()
    {
        var storeUpdatables = GetStoreUpdatables();
        
        foreach (var storeUpdatable in storeUpdatables)
            storeUpdatable.UpdateStore(Store);
    }
    
    private IEnumerable<IStoreUpdatable<TStore>> GetStoreUpdatables()
    {
        var storeUpdatableProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(IsStoreUpdatable);
            
        return storeUpdatableProperties.Select(GetStoreUpdatable)
            .Where(value => value is not null);
    }

    private bool IsStoreUpdatable(PropertyInfo property)
    {
        foreach (var @interface in property.PropertyType.GetInterfaces())
        {
            if (!@interface.IsGenericType)
                continue;

            if (@interface.GetGenericTypeDefinition() == typeof(IStoreUpdatable<>))
                return true;
        }

        return false;
    }

    private IStoreUpdatable<TStore> GetStoreUpdatable(PropertyInfo property) =>
        (IStoreUpdatable<TStore>)property.GetMethod?.Invoke(this, Array.Empty<object>())!;
}