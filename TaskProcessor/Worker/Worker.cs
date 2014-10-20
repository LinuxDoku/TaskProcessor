using System;
using System.Threading;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contract.Worker;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Worker {
    /// <summary>
    /// A simple worker for ITaskQueue.
    /// </summary>
    [Export(typeof(IWorker))]
    public class Worker : IWorker {
        private Thread _thread;
        private WorkerStatus _status = WorkerStatus.Waiting;

        public WorkerStatus Status {
            get {
                return _status;
            }
        }

        public void Execute(ITaskExecution taskExecution) {
            _thread = new Thread(() => {
                _status = WorkerStatus.Working;
                taskExecution.Status = TaskStatus.Running;

                try {
                    taskExecution.Task.Execute(taskExecution.Configuration);
                    taskExecution.Status = TaskStatus.Successful;
                } catch (Exception exception) {
                    taskExecution.Log(exception);
                    taskExecution.Status = TaskStatus.Failed;
                }
                
                _status = WorkerStatus.Waiting;
            });
            _thread.Start();
        }

        /// <summary>
        /// Start the worker.
        /// </summary>
        public void Start() {
            if (_thread.ThreadState == ThreadState.Aborted) {
                _thread.Start();
            }
        }

        /// <summary>
        /// Stop the worker. Now.
        /// </summary>
        public void Stop() {
            _thread.Abort();
            _status = WorkerStatus.Stopped;
        }

        /// <summary>
        /// Cancels the worker execution when it's work is done.
        /// </summary>
        public void Cancel() {
            if (_status == WorkerStatus.Working) {
                _status = WorkerStatus.Canceled;
            }
        }

        /// <summary>
        /// Abort a prior worker cancel.
        /// </summary>
        public void AbortCancel() {
            if (_status == WorkerStatus.Canceled) {
                if (_thread.ThreadState == ThreadState.Running) {
                    _status = WorkerStatus.Working;
                } else {
                    _status = WorkerStatus.Waiting;
                }
            }
        }
    }
}