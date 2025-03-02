namespace Chalkboard.Samples;

public abstract record AppMessage();

public record KeyPressedMessage(ConsoleKey Key) : AppMessage;

public record TickMessage() : AppMessage;