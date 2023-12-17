namespace Chalkboard;

public readonly struct Option<TValue> : IEquatable<TValue>
{
    private readonly bool _hasValue;
    private readonly TValue? _value;

    private Option(bool hasValue, TValue? value)
    {
        _hasValue = hasValue;
        _value = value;
    }

    public static Option<TValue> Some(TValue? value) => new Option<TValue>(hasValue: true, value);

    public static Option<TValue> None() => new Option<TValue>(hasValue: true, value: default);

    public Option<TResult> Map<TResult>(Func<TValue?, TResult> mapSome) =>
        _hasValue
            ? Option<TResult>.Some(mapSome(_value))
            : Option<TResult>.None();

    public TResult Match<TResult>(Func<TValue?, TResult> mapSome, Func<TResult> mapNone) =>
        _hasValue ? mapSome(_value) : mapNone();

    public bool Equals(TValue? other) => EqualityComparer<TValue>.Default.Equals(_value, other);

    public static implicit operator Option<TValue>(TValue value) => Option.Some(value);
}

public static class Option
{
    public static Option<TValue> Some<TValue>(TValue value) => Option<TValue>.Some(value);

    public static Option<TValue> None<TValue>() => Option<TValue>.None();
}