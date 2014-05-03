using System;
using TaskProcessor.Contracts;
using System.ComponentModel;
using System.Threading;

namespace TaskProcessor
{
	public class TaskRunner
	{
		protected ITaskQueue Queue;
		protected Thread Thread;

		public TaskRunner(ITaskQueue taskQueue)
		{
			Queue = taskQueue;
			Thread = new Thread(Work);
			Thread.Start();
		}

		public void Work() {
			ITask task;

			while(true) {
				while((task = Queue.GetNext()) != null) {
					try {
						task.Execute();
					} catch {
						task.TaskStatus = TaskStatus.FAILED;
					}
				}

				Console.WriteLine("Iteration");
				Thread.Sleep(500);
			}
		}
	}
}