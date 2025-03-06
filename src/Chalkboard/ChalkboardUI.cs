using Chalkboard;
using Chalkboard.Samples;

public class ChalkboardUI<TStore> : IDisposable
{
    private readonly TaskHandlingLoop _messageLoop = new();

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

    public Action<TMessage> AddMessage<TMessage>(StoreReducer<TStore, TMessage> storeReducer)
    {
        var messageReducer = _store.CreateReducer(storeReducer);
        return message => _messageLoop.AddTask(() => messageReducer(message));
    }

    public void EnterMessageLoop(CancellationToken cancellationToken = default)
    {
        _messageLoop.RunLoop(cancellationToken);
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