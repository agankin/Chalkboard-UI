namespace Chalkboard;

public delegate TStore StoreReducer<TStore, TAction>(TStore store, TAction action);