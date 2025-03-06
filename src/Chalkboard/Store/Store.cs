namespace Chalkboard;

public class Store<TStore>
{
    private readonly Action _onUpdated;

    public Store(TStore initialValue, Action onUpdated)
    {
        Value = initialValue;
        _onUpdated = onUpdated;
    }

    public TStore Value { get; private set; }

    public ActionDispatcher<TAction> CreateReducer<TAction>(StoreReducer<TStore, TAction> reducer)
    {
        return action =>
        {
            Value = reducer(Value, action);
            _onUpdated();
        };
    }

    public static implicit operator TStore(Store<TStore> store) => store.Value;
}