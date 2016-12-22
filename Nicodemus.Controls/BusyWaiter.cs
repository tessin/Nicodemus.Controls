using System;
using System.Threading;

namespace Nicodemus.Controls
{
    /// <summary>
    /// Class used to busy wait on a main thread as a task is completing 
    /// on a separate thread. This is useful in Lightswitch for displaying
    /// the busy indicator while an asynchronous task (such as web service 
    /// call) is completing in the background. This to prevent further user
    /// action until the process is complete.
    ///    
    /// Usage: Instantiate and call Start() on the main thread and Cancel()
    /// on the worker thread.
    /// </summary>
    public class BusyWaiter
    {

        private readonly TimeSpan _timeout;

        public bool Cancelled { get; private set; }

        public TimeSpan Elapsed { get; private set; }

        public BusyWaiter(): this(30000)
        {
        }

        public BusyWaiter(TimeSpan timeout)
        {
            _timeout = timeout;
        }

        public BusyWaiter(int timeout)
        {
            _timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public void Start()
        {
            if (Cancelled) return;
            var started = DateTime.UtcNow;
            do
            {
                Thread.Sleep(100);
            } while (DateTime.UtcNow < (started + _timeout) && !Cancelled);
            Elapsed = DateTime.UtcNow - started;
        }

        public void Cancel()
        {
            Cancelled = true;
        }

    }
}
