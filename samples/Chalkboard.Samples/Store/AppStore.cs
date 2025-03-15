using System.Collections.Immutable;

namespace Chalkboard.Samples;

public record AppStore
{
    public required Size FieldSize { get; init; }

    public required SnakeModel Snake { get; init; }

    public required SnakeDirection Direction { get; init; }

    public IEnumerable<Position> Obstacles { get; init; }

    public required bool Collided { get; init; }
}