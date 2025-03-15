namespace Chalkboard;

public delegate IEnumerable<Element<TStore>> GetChildrenFunc<TStore>(TStore store);