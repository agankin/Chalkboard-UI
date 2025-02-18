namespace Chalkboard;

public class ComputedValue<TStore, TValue> : IStoreUpdatable<TStore>
{
    private readonly Func<TStore, TValue> _compute;

    public ComputedValue(Func<TStore, TValue> compute) => _compute = compute;

    public TValue Value { get; private set; } = default!;

    public void UpdateStore(TStore store)
    {
        Value = _compute(store);
        
        if (Value is IStoreUpdatable<TStore> storeUpdatable)
            storeUpdatable.UpdateStore(store);
    }

    public static implicit operator ComputedValue<TStore, TValue>(TValue value) => new(_ => value);

    public static implicit operator ComputedValue<TStore, TValue>(Func<TStore, TValue> compute) => new(compute);

    public static implicit operator TValue(ComputedValue<TStore, TValue> computedValue) => computedValue.Value;
}