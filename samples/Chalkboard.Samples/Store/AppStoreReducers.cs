namespace Chalkboard.Samples;

using static SnakeModelTransformers;

public static class AppStoreReducers
{
    public static AppStore OnTick(AppStore store, Nothing _) => OnTick(store);

    public static AppStore OnTick(AppStore store)
    {
        Transformer<SnakeModel> snakeTransformer = store.Direction switch
        {
            SnakeDirection.Up => MoveUp,
            SnakeDirection.Down => MoveDown,
            SnakeDirection.Left => MoveLeft,
            SnakeDirection.Right => MoveRight,
            _ => throw new InvalidOperationException($"Unsupported {nameof(SnakeDirection)} enum value: {store.Direction}.")
        };

        var snake = snakeTransformer(store.Snake);
            

        return InBounds(snake, store.FieldSize)
            ? store with { Snake = snakeTransformer(store.Snake) }
            : store;
    }

    private static bool InBounds(SnakeModel snake, Size size)
    {
        return Rect.WithSize(size).Contains(snake.Head);
    }
}