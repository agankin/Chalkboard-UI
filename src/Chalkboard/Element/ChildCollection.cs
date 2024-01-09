using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Chalkboard;

public class ChildCollection : ObservableCollection<Element>
{
    private readonly Element _parent;

    public ChildCollection(Element parent)
    {
        _parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    public event EventHandler<ElementsChangedEventArgs>? ElementsChanged;

    public event EventHandler<RenderRequiredEventArgs>? ChildRenderRequired;

    public void AddRange(IEnumerable<Element> children)
    {
        foreach (var child in children)
            Add(child);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        base.OnCollectionChanged(args);

        var addedElements = AsElements(args.NewItems);
        ValidateNotAttached(addedElements);
        Attach(addedElements);
        SubscribeRenderRequiredEvent(addedElements);

        var removedElements = AsElements(args.OldItems);
        Detach(removedElements);
        UnsubscribeRenderRequiredEvent(removedElements);

        RaiseElementsChanged(addedElements, removedElements);
    }

    private void ValidateNotAttached(IEnumerable<Element> addedElements)
    {
        if (addedElements.Any(el => el.Parent != null))
            throw new Exception("An added element is already child of another parent.");
    }

    private void Attach(IEnumerable<Element> elements)
    {
        foreach (var el in elements)
            el.Parent = _parent;
    }

    private void Detach(IEnumerable<Element> elements)
    {
        foreach (var el in elements)
            el.Parent = null;
    }

    private void SubscribeRenderRequiredEvent(IEnumerable<Element> elements)
    {
        foreach (var element in elements)
            element.RenderRequired += OnChildRenderRequired;
    }

    private void UnsubscribeRenderRequiredEvent(IEnumerable<Element> elements)
    {
        foreach (var element in elements)
            element.RenderRequired -= OnChildRenderRequired;
    }

    private void OnChildRenderRequired(object? _, RenderRequiredEventArgs args) => RaiseChildRenderRequired(args);

    private void RaiseElementsChanged(IEnumerable<Element> addedElements, IEnumerable<Element> removedElements)
    {
        var eventArgs = new ElementsChangedEventArgs(addedElements, removedElements);
        ElementsChanged?.Invoke(this, eventArgs);
    }

    private void RaiseChildRenderRequired(RenderRequiredEventArgs childArgs)
    {
        ChildRenderRequired?.Invoke(this, childArgs);
    }

    private static IEnumerable<Element> AsElements(IList? elements) =>
        elements?.Cast<Element>() ?? Enumerable.Empty<Element>();
}