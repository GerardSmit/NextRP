using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NextFramework.Helpers
{
    internal class TickScheduler : TaskScheduler
    {
        public static TickScheduler Instance { get; private set; }

        public static void Initialize()
        {
            Instance = new TickScheduler();
        }

        private readonly ConcurrentQueue<Task> _tasks = new ConcurrentQueue<Task>();
        private readonly int _mainThreadId;

        public TickScheduler()
        {
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public override int MaximumConcurrencyLevel { get; } = 1;

        public bool IsMainThread => Thread.CurrentThread.ManagedThreadId == _mainThreadId;

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks;
        }

        protected override void QueueTask(Task task)
        {
            _tasks.Enqueue(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!IsMainThread)
            {
                return false;
            }

            return TryExecuteTask(task);
        }

        internal void Tick()
        {
            var runs = _tasks.Count;

            while (runs-- > 0 && _tasks.TryDequeue(out Task task))
            {
                TryExecuteTask(task);
            }
        }

        internal Task Schedule(Action action, bool forceSchedule = false)
        {
            if (forceSchedule == false && IsMainThread)
            {
                try
                {
                    action();

                    return Task.CompletedTask;
                }
                catch (Exception e)
                {
                    return Task.FromException(e);
                }
            }

            return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, this);
        }

        internal Task<T> Schedule<T>(Func<T> action, bool forceSchedule = false)
        {
            if (forceSchedule == false && IsMainThread)
            {
                try
                {
                    return Task.FromResult(action());
                }
                catch (Exception e)
                {
                    return Task.FromException<T>(e);
                }
            }

            return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, this);
        }
    }
}
