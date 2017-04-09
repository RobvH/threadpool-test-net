using System;
using System.Threading;

namespace Threadtest
{
    public class ThreadableTask
    {
        private int threadCount;
        private ManualResetEvent completed;

        public void Process()
        {
            DispatchThreads();

            WaitForAllThreadsToComplete();
        }

        private void WaitForAllThreadsToComplete()
        {
            completed.WaitOne();
        }

        private void DispatchThreads()
        {
            completed = new ManualResetEvent(false);

            for (var i = 0; i < 150; ++i) {
                var threadWorker = new ThreadWorker(i, SignalThreadCompleted);

                Interlocked.Increment(ref threadCount);

                ThreadPool.QueueUserWorkItem(threadWorker.ThreadPoolCallback);
            }
        }

        internal void SignalThreadCompleted()
        {
            if (completed == null) {
                throw new Exception("SignalThreadCompleted called but sync object not initialized.");
            }

            if (Interlocked.Decrement(ref threadCount) == 0) {
                completed.Set();
            }
        }
    }
}