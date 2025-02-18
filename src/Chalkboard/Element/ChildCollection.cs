using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Chalkboard;

public class ChildCollection<TStore> : ObservableCollection<Element<TStore>>, IStoreUpdatable<TStore>
{
    private readonly Element<TStore> _parent;

    public ChildCollection(Element<TStore> parent)
    {
        _parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    public void UpdateStore(TStore store)
    {
        foreach (var child in this)
            child.UpdateStore(store);
    }

    public void AddRange(IEnumerable<Element<TStore>> children)
    {
        foreach (var child in children)
            Add(child);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        base.OnCollectionChanged(args);

        var addedElements = AsElements(args.NewItems);
        Attach(addedElements);
        
        var removedElements = AsElements(args.OldItems);
        Detach(removedElements);
    }

    private void Attach(IEnumerable<Element<TStore>> addedElements)
    {
        ValidateNotAttached(addedElements);

        foreach (var element in addedElements)
        {
            element.Parent = _parent;
            element.UpdateStore(_parent.Store);
        }
    }

    private void ValidateNotAttached(IEnumerable<Element<TStore>> addedElements)
    {
        var childOfAnotherParent = addedElements.FirstOrDefault(el => el.Parent != null);
        if (childOfAnotherParent != null)
            throw new Exception($"An added element {childOfAnotherParent} is already child of another parent.");
    }

    private void Detach(IEnumerable<Element<TStore>> removedElements)
    {
        foreach (var element in removedElements)
            element.Parent = null;
    }

    private static IEnumerable<Element<TStore>> AsElements(IList? elements)
    {
        return elements?.Cast<Element<TStore>>() ?? [];
    }
}