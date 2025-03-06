using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Chalkboard;

public class StoreUpdatableCollection<TStore, TStoreUpdatable> : ObservableCollection<TStoreUpdatable>, IStoreUpdatable<TStore>
    where TStoreUpdatable : IStoreUpdatable<TStore>
{
    private TStore _store = default!;

    public void UpdateStore(TStore store)
    {
        foreach (var child in this)
            child.UpdateStore(store);
    }

    public void AddRange(IEnumerable<TStoreUpdatable> children)
    {
        foreach (var child in children)
            Add(child);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        base.OnCollectionChanged(args);

        var addedElements = AsElements(args.NewItems);
        addedElements.ForEach(el => el.UpdateStore(_store));
    }

    private static IEnumerable<Element<TStore>> AsElements(IList? elements)
    {
        return elements?.Cast<Element<TStore>>() ?? [];
    }
}