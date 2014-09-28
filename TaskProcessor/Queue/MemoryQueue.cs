using TaskProcessor.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;
using System;

namespace TaskProcessor.Queue {
    /// <summary>
    /// In memory task queue.
    /// </summary>
    public class MemoryQueue : ITaskQueue {
        private readonly Thread _thread;
        private readonly IList<IWorker> _workers;
        private readonly ConcurrentBag<ITaskExecution> _tasks;

        public MemoryQueue() {
            _workers = new List<IWorker>();
            _tasks = new ConcurrentBag<ITaskExecution>();

            _thread = new Thread(Process);
            _thread.Start();
        }

        #region ITaskList implementation


        public void Add(ITaskExecution task) {
            task.Status = TaskStatus.QUEUED;
            _tasks.Add(task);
        }

        public void Add(IWorker worker) {
            _workers.Add(worker);
        }

        public void SetWorkerStatus(IWorker worker, WorkerStatus workerStatus) {
            if (!_workers.Contains(worker)) {
                _workers.Add(worker);
            }
        }

        /// <summary>
        /// Get all queued tasks.
        /// </summary>
        /// <returns>The all.</returns>
        public IEnumerable<ITaskExecution> GetAll() {
            return _tasks.ToArray();
        }

        public IEnumerable<IWorker> GetAllWorkers() {
            return _workers.ToArray();
        }

        #endregion

        private void Process() {
            while (true) {
                var task = GetNextTask();
                if (task != null) {
                    var worker = _workers.FirstOrDefault(x => x.GetStatus() == WorkerStatus.WAITING);
                    if (worker != null) {
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

        private ITaskExecution GetNextTask() {
            return _tasks.FirstOrDefault(
                x => x.Status == TaskStatus.QUEUED && (x.StartTime == default(DateTime) || (x.StartTime <= DateTime.Now))
            );
        }
    }
}