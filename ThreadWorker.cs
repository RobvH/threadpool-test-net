using System;

namespace Threadtest
{
    public class ThreadWorker
    {
        private readonly int threadId;
        private readonly Action signalThreadCompleted;

        public ThreadWorker(int threadId, Action signalThreadCompleted)
        {
            this.threadId = threadId;
            this.signalThreadCompleted = signalThreadCompleted;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            try {
                // Thread.Sleep(1000); // enable to demonstrate effect of low initial min thread counts
                Console.Out.WriteLine("Hello from thread " + threadId + "!");
            }
            finally {
                signalThreadCompleted();
            }
        }
    }
}