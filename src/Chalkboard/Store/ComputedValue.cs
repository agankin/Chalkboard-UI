namespace Chalkboard;

public class ComputedValue<TStore, TValue> : IStoreUpdatable<TStore>
{
    private readonly Func<TStore, TValue> _compute;
    private TStore _store = default!;

    public ComputedValue(Func<TStore, TValue> compute) => _compute = compute;

    public TValue Value => _compute(_store);

    public void UpdateStore(TStore store) => _store = store;

    public static implicit operator ComputedValue<TStore, TValue>(TValue value) => new(_ => value);

    public static implicit operator ComputedValue<TStore, TValue>(Func<TStore, TValue> compute) => new(compute);

    public static implicit operator TValue(ComputedValue<TStore, TValue> computedValue) => computedValue.Value;
}