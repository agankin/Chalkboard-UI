namespace Chalkboard;

public abstract class ContentElement<TStore> : Element<TStore>
{
    public ContentElement()
    {
        AutoUpdateStoreUpdatable(() => Content);
    }

    public ComputedValue<TStore, Element<TStore>> Content { get; set; } = null!;
}