namespace Chalkboard;

public class Store<TStore>
{
    private readonly ChalkboardUI<TStore> _ui;

    public Store(ChalkboardUI<TStore> ui, TStore initialValue)
    {
        _ui = ui;
        Value = initialValue;
    }

    public TStore Value { get; private set; }

    public void Update(Func<TStore, TStore> transform)
    {
        Value = transform(Value);

        _ui.NotifyStoreUpdated();
        _ui.Render();
    }
}