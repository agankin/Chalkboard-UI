
namespace Chalkboard;

public class FuncLayoutElement<TStore> : LayoutElement<TStore>
{
    private readonly GetChildrenFunc<TStore> _getChildren;

    public FuncLayoutElement(GetChildrenFunc<TStore> getChildren)
    {
        _getChildren = getChildren;
    }

    protected override IEnumerable<Element<TStore>> GetChildren() => _getChildren(Store);
}