using System.Collections.Concurrent;

namespace Chalkboard.Samples;

internal class TaskHandlingLoop
{
    private const int InfiniteTimeout = -1;

    private readonly BlockingCollection<Action> _taskQueue = new();

    public void RunLoop(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (!RunOnce(cancellationToken))
                return;
        }
    }

    private bool RunOnce(CancellationToken cancellationToken)
    {
        bool runNext;
        try
        {
            if (runNext = _taskQueue.TryTake(out Action task, InfiniteTimeout, cancellationToken))
                InvokeSafe(task);

            return runNext;

        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }

    private static void InvokeSafe(Action task)
    {
        try
        {
            task.Invoke();
        }
        catch {}
    }

    public void AddTask(Action task) => _taskQueue.Add(task);
}