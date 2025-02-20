using System.Collections.Concurrent;

namespace Chalkboard.Samples;

public class AppMessageLoop
{
    private const int InfiniteTimeout = -1;

    private readonly BlockingCollection<AppMessage> _messageQueue = new();

    public void OnMessage(Action<AppMessage> onMessage)
    {
        while (true)
        {
            if (_messageQueue.TryTake(out AppMessage? message, InfiniteTimeout))
            {
                if (IsEscapeKeyMessage(message))
                    return;

                onMessage(message);
            }
        }
    }

    public void Add(AppMessage message) => _messageQueue.Add(message);

    private static bool IsEscapeKeyMessage(AppMessage? message)
    {
        return message is KeyPressedMessage keyPressedMessage && keyPressedMessage.Key == ConsoleKey.Escape;
    }
}