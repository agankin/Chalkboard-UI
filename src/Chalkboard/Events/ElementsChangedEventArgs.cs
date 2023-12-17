namespace Chalkboard;

public class ElementsChangedEventArgs : EventArgs
{
    public ElementsChangedEventArgs(IEnumerable<Element> addedElements, IEnumerable<Element> removedElements)
    {
        AddedElements = addedElements;
        RemovedElements = removedElements;
    }

    public IEnumerable<Element> AddedElements { get; }

    public IEnumerable<Element> RemovedElements { get; }
}