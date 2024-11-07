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

    public Action<TArg> CreateReducer<TArg>(StoreReducer<TStore, TArg> reducer)
    {
        return arg =>
        {
            Value = reducer(Value, arg);
            _onUpdated();
        };
    }

    public static implicit operator TStore(Store<TStore> store) => store.Value;
}