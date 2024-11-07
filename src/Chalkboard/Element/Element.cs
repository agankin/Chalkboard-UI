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
        var storeUpdatables = SelectStoreUpdatables();
        
        foreach (var storeUpdatable in storeUpdatables)
            storeUpdatable.UpdateStore(Store);
    }
    
    private IEnumerable<IStoreUpdatable<TStore>> SelectStoreUpdatables()
    {
        var storeUpdatableProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(IsStoreUpdatable);
            
        return storeUpdatableProperties.Where(property => property.GetMethod is not null)
            .Select(property => property.GetMethod?.Invoke(this, Array.Empty<object>()))
            .Where(value => value is not null)
            .Cast<IStoreUpdatable<TStore>>();
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
}