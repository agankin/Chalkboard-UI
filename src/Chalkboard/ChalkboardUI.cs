using Chalkboard;
using Chalkboard.Samples;

public class ChalkboardUI<TStore> : IDisposable
{
    private readonly TaskHandlingLoop _taskLoop = new();

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

    public ActionDispatcher<TMessage> AddStoreReducer<TMessage>(StoreReducer<TStore, TMessage> storeReducer)
    {
        var actionDispatcher = _store.CreateReducer(storeReducer);
        return action => _taskLoop.AddTask(() => actionDispatcher(action));
    }

    public void EnterActionLoop(CancellationToken cancellationToken = default)
    {
        _taskLoop.RunLoop(cancellationToken);
    }

    private void Render()
    {
        _root.UpdateStore(_store.Value);
        var renderedRect = _root.Render(_renderer.Size);

        _renderer.Render(renderedRect);
    }

    public void Dispose()
    {
    }

    private void OnStoreUpdated() => Render();
}