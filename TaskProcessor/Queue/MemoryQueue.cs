using TaskProcessor.Contract.Queue;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contract.Worker;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Queue {
    /// <summary>
    /// In memory task queue.
    /// </summary>
    [Export(typeof(ITaskQueue))]
    public class MemoryQueue : ITaskQueue {
        private readonly IList<IWorker> _workers;
        private readonly List<ITaskExecution> _tasks;

        public MemoryQueue() {
            _workers = new List<IWorker>();
            _tasks = new List<ITaskExecution>();

            new Thread(Process).Start();
        }

        public string Name { get; set; }

        /// <summary>
        /// Schuedule a new task.
        /// </summary>
        /// <param name="task"></param>
        public void Add(ITaskExecution task) {
            task.Status = TaskStatus.Queued;
            _tasks.Add(task);
        }

        public void Add(IEnumerable<ITaskExecution> tasks) {
            foreach (var task in tasks) {
                Add(task);
            }
        }

        public void Add(IWorker worker) {
            _workers.Add(worker);
        }

        public void Add(IEnumerable<IWorker> workers) {
            foreach (var worker in workers) {
                Add(worker);
            }
        }

        public void SetWorkerStatus(IWorker worker, WorkerStatus workerStatus) {
            if (!_workers.Contains(worker)) {
                _workers.Add(worker);
            }
        }

        public IEnumerable<ITaskExecution> Tasks  {
            get { return _tasks; }
        }

        public IEnumerable<IWorker> Workers {
            get { return _workers; }
        }

        private void Process() {
            while (true) {
                var task = GetNextTask();
                if (task != null) {
                    var worker = _workers.FirstOrDefault(x => x.Status == WorkerStatus.Waiting);
                    if (worker != null) {
                        task.Status = TaskStatus.Waiting;
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
                x => x.Status == TaskStatus.Queued && (x.StartTime == default(DateTime) || (x.StartTime <= DateTime.Now))
            );
        }
    }
}