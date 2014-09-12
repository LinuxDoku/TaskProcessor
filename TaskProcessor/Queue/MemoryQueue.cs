using TaskProcessor.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;
using System;

namespace TaskProcessor.Queue
{
	/// <summary>
	/// In memory task queue.
	/// </summary>
	public class MemoryQueue : ITaskQueue
	{
        protected Thread Thread;
        protected IList<IWorker> Workers;
        protected ConcurrentBag<ITaskExecution> Tasks;

		public MemoryQueue()
		{
            Workers = new List<IWorker>();
            Tasks = new ConcurrentBag<ITaskExecution>();

            Thread = new Thread(Process);
            Thread.Start();
		}

        #region ITaskList implementation


        public void Add(ITaskExecution task)
        {
            task.Status = TaskProcessor.Contracts.TaskStatus.QUEUED;
            Tasks.Add(task);
        }

        public void Add(IWorker worker) 
        {
            Workers.Add(worker);
        }

        public void SetWorkerStatus(IWorker worker, WorkerStatus workerStatus)
        {
            if (!Workers.Contains(worker))
            {
                Workers.Add(worker);
            }
        }
            
        /// <summary>
        /// Get all queued tasks.
        /// </summary>
        /// <returns>The all.</returns>
        public IEnumerable<ITaskExecution> GetAll()
        {
            return Tasks.ToArray();
        }

        public IEnumerable<IWorker> GetAllWorkers()
        {
            return Workers.ToArray();
        }

		#endregion

        protected void Process() 
        {
            while (true)
            {
                var task = GetNextTask();
                if (task != null)
                {
                    var worker = Workers.FirstOrDefault(x => x.GetStatus() == WorkerStatus.WAITING);
                    if (worker != null)
                    {
                        task.Status = TaskStatus.WAITING;
                        worker.Execute(task);
                    } else {
                        Thread.Sleep(500);
                    }
                } else {
                    Thread.Sleep(500);
                }
            }
        }

        protected ITaskExecution GetNextTask() 
        {
            return Tasks.FirstOrDefault(
                x => x.Status == TaskStatus.QUEUED
                // TODO: add time specific execution
            );
        }
	}
}