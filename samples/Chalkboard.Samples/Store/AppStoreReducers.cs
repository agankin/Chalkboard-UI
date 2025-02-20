using System.Collections.Immutable;

namespace Chalkboard.Samples;

using static SnakeModelTransformers;

public static class AppStoreReducers
{
    private static readonly Random Random = new();
    private static readonly ImmutableList<SnakeDirection> AllDirections = [
        SnakeDirection.Up,
        SnakeDirection.Down,
        SnakeDirection.Left,
        SnakeDirection.Right
    ];

    public static AppStore OnMessage(AppStore store, AppMessage message)
    {
        switch (message)
        {
            case TickMessage _:
                return OnTick(store);

            case KeyPressedMessage keyPressedMessage:
                return OnKeyPressed(store, keyPressedMessage.Key);

            default:
                return store;
        }
    }

    private static AppStore OnTick(AppStore store)
    {
        var direction = store.Direction;

        var snake = GetMoveTransformer(direction).Invoke(store.Snake);
        if (IsValid(snake, store.FieldSize))
            return store with { Snake = snake };

        return MoveChangingDirection(store);
    }

    private static AppStore OnKeyPressed(AppStore store, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                return UpdateDirection(store, SnakeDirection.Up);

            case ConsoleKey.DownArrow:
                return UpdateDirection(store, SnakeDirection.Down);

            case ConsoleKey.LeftArrow:
                return UpdateDirection(store, SnakeDirection.Left);

            case ConsoleKey.RightArrow:
                return UpdateDirection(store, SnakeDirection.Right);

            default:
                return store;
        }
    }

    private static AppStore UpdateDirection(AppStore store, SnakeDirection direction)
    {
        return store with { Direction = direction };
    }

    private static AppStore MoveChangingDirection(AppStore store, ImmutableList<SnakeDirection> triedDirections = null!)
    {
        triedDirections ??= AllDirections.Remove(store.Direction);
        if (triedDirections.Count == 0)
            return store;
        
        var selectedDirection = SelectRandomly(triedDirections);
        var snake = GetMoveTransformer(selectedDirection).Invoke(store.Snake);
        
        if (IsValid(snake, store.FieldSize))
            return store with { Snake = snake, Direction = selectedDirection };

        return MoveChangingDirection(store, triedDirections.Remove(selectedDirection));
    }

    private static SnakeDirection SelectRandomly(ImmutableList<SnakeDirection> triedDirections)
    {
        var idx = Random.Next(triedDirections.Count);
        return triedDirections[idx];
    }

    private static bool IsValid(SnakeModel snake, Size fieldSize) => !OutOfBounds(snake, fieldSize) && !SelfIntersects(snake);

    private static bool OutOfBounds(SnakeModel snake, Size fieldSize) => !Rect.WithSize(fieldSize).Contains(snake.Head);

    private static bool SelfIntersects(SnakeModel snake) => snake.Tail.Contains(snake.Head);
}