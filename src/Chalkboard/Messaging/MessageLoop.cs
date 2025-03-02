using System.Collections.Concurrent;

namespace Chalkboard.Samples;

public class MessageLoop<TMessage>
{
    private const int InfiniteTimeout = -1;

    private readonly BlockingCollection<TMessage> _messageQueue = new();
    private readonly Predicate<TMessage> _isExitMessage;

    public MessageLoop(Predicate<TMessage> isExitMessage)
    {
        _isExitMessage = isExitMessage;
    }

    public void OnMessage(Action<TMessage> onMessage)
    {
        while (true)
        {
            if (_messageQueue.TryTake(out TMessage message, InfiniteTimeout))
            {
                if (_isExitMessage(message))
                    return;

                onMessage(message);
            }
        }
    }

    public void Add(TMessage message) => _messageQueue.Add(message);
}