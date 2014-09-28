using System.Threading;
using System;
using TaskProcessor.Contracts;

namespace TaskProcessor
{
	/// <summary>
	/// A simple worker for ITaskQueue.
	/// </summary>
    public class Worker : IWorker
	{
		protected Thread Thread;
		protected bool Canceled = false;
        protected WorkerStatus Status = WorkerStatus.WAITING;

        public WorkerStatus GetStatus() 
        {
            return Status;
        }
            
        public void Execute(ITaskExecution taskExecution)
        {
            Thread = new Thread(() =>
            {
                Status = WorkerStatus.WORKING;
                taskExecution.Status = TaskStatus.RUNNING;
                try
                {
                    taskExecution.Task.Execute();
                    taskExecution.Status = TaskStatus.SUCCESSFUL;
                }
                catch (Exception exception)
                {
                    taskExecution.Log(exception);
                    taskExecution.Status = TaskStatus.FAILED;
                }
                Status = WorkerStatus.WAITING;
            });
            Thread.Start();
        }


		/// <summary>
		/// Start the worker.
		/// </summary>
		public void Start()
		{
            throw new NotImplementedException();
		}

		/// <summary>
		/// Stop the worker. Now.
		/// </summary>
		public void Stop()
		{
			Thread.Abort();
		}

		/// <summary>
		/// Cancels the worker execution when it's work is done.
		/// </summary>
		public void Cancel()
		{
			Canceled = true;
		}

		/// <summary>
		/// Abort a prior worker cancel.
		/// </summary>
		public void AbortCancel() {
			Canceled = false;
		}
	}
}