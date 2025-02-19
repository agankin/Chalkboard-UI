using System.Collections;
using System.Collections.Immutable;

namespace Chalkboard.Samples;

public record SnakeModel(ImmutableList<Position> body) : IEnumerable<Position>
{
    private readonly ImmutableList<Position> _body = body;
    
    public SnakeModel(params Position[] body) : this(ToImmutableList(body))
    {
    }

    public Position Head => _body[0];

    public SnakeModel Move(Func<Position, Position> moveHead)
    {
        var newBody = _body.RemoveAt(_body.Count - 1)
            .Insert(0, moveHead(Head));

        return new(newBody);
    }

    private static ImmutableList<Position> ToImmutableList(IReadOnlyList<Position> body)
    {
        if (body == null)
            throw new ArgumentNullException(nameof(body));

        if (body.Count == 0)
            throw new ArgumentException("Snake must have body.", nameof(body));

        return ImmutableList<Position>.Empty.AddRange(body);
    }

    public IEnumerator<Position> GetEnumerator() => _body.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}