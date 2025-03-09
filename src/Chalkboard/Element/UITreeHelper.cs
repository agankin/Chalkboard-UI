namespace Chalkboard;

public static class UITreeHelper<TStore>
{
    public static TElement _<TElement>() where TElement : Element<TStore>, new()
    {
        return new TElement();
    }
    
    public static TElement _<TElement>(Element<TStore> content) where TElement : ContentElement<TStore>, new()
    {
        var element = new TElement
        {
            Content = content
        };

        return element;
    }

    public static TElement _<TElement>(IEnumerable<Element<TStore>> children) where TElement : LayoutElement<TStore>, new()
    {
        return _<TElement>(children.ToArray());
    }
    
    public static TElement _<TElement>(params Element<TStore>[] children) where TElement : LayoutElement<TStore>, new()
    {
        var element = new TElement();
        element.Children.AddRange(children);

        return element;
    }
}