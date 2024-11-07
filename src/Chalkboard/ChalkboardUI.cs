using Chalkboard;

public class ChalkboardUI<TStore> : IDisposable
{
    private readonly Element<TStore> _root;
    private readonly IRenderer _renderer;
    private readonly Store<TStore> _store;

    public ChalkboardUI(Element<TStore> root, IRenderer renderer, TStore initialStore)
    {
        _root = root;
        _renderer = renderer;
        _store = new Store<TStore>(initialStore, OnStoreUpdated);
        
        _root.UpdateStore(_store.Value);
        _renderer.SizeChanged += Render;

        Render();
    }

    public Action<TArg> CreateStoreReducer<TArg>(StoreReducer<TStore, TArg> reducer) => _store.CreateReducer(reducer);

    public void Render()
    {
        _root.UpdateStore(_store.Value);
        var renderedRect = _root.Render(_renderer.GetRenderingSize());

        _renderer.Render(renderedRect);
    }

    public void Dispose()
    {
    }

    private void OnStoreUpdated() => Render();
}