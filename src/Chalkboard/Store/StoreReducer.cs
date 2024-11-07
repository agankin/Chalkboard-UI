namespace Chalkboard;

public delegate TStore StoreReducer<TStore, TArg>(TStore store, TArg arg);