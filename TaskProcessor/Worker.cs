using TaskProcessor.Contracts;
using System.Threading;
using System;

namespace TaskProcessor
{
	/// <summary>
	/// A simple worker for ITaskQueue.
	/// </summary>
	public class Worker
	{
		protected ITaskQueue Queue;
		protected Thread Thread;
		protected bool Canceled = false;

		/// <summary>
		/// Initialize and start the worker.
		/// </summary>
		/// <param name="taskQueue">Task queue.</param>
		public Worker(ITaskQueue taskQueue)
		{
			Queue = taskQueue;
			Start();
		}

		/// <summary>
		/// Start the worker.
		/// </summary>
		public void Start()
		{
			Thread = new Thread(Work);
			Thread.Start();
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

		/// <summary>
		/// Let the worker do it's job.
		/// </summary>
		protected void Work()
		{
			ITask task;

			while(true) {
				while((task = Queue.GetNext()) != null) {
					try {
						task.Execute();
					} catch {
						task.TaskStatus = TaskStatus.FAILED;
					}
				}

				if(Canceled) {
					Thread.Abort();
					return;
				}
			
				Console.WriteLine("tickk");
				Thread.Sleep(500);
			}
		}
	}
}