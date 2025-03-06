namespace Chalkboard;

public interface IStoreUpdatable<TStore>
{
    public void UpdateStore(TStore store);
}