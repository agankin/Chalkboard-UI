namespace Chalkboard;

public static class OptionExtensions
{
    public static TValue? ValueOr<TValue>(this Option<TValue> option, TValue? alternativeValue) =>
        option.Match(value => value, () => alternativeValue);
}