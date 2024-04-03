using Chalkboard;

public class ChalkboardUI<TStore>
{
    private readonly IRenderer _renderer;
    private readonly Element<TStore> _root;

    public ChalkboardUI(Func<Store<TStore>, Element<TStore>> createRoot, IRenderer renderer, TStore initialStoreValue)
    {
        Store = new Store<TStore>(this, initialStoreValue);

        _root = createRoot(Store);
        _renderer = renderer;
    }

    public Store<TStore> Store { get; }
    
    public void Render()
    {
        var renderedRect = _root.Render();
        _renderer.Render(renderedRect);
    }

    internal void NotifyStoreUpdated() => _root.StoreUpdated();
}