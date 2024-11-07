namespace Chalkboard;

public abstract class ContentElement<TStore> : Element<TStore>
{
    public ComputedValue<TStore, Element<TStore>> Content { get; set; } = null!;
}