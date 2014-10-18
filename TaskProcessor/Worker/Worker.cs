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
        private bool _canceled = false;
        private WorkerStatus _status = WorkerStatus.WAITING;

        public WorkerStatus Status {
            get {
                return _status;
            }
        }

        public void Execute(ITaskExecution taskExecution) {
            _thread = new Thread(() => {
                _status = WorkerStatus.WORKING;
                taskExecution.Status = TaskStatus.RUNNING;
                try {
                    taskExecution.Task.Execute(taskExecution.Configuration);
                    taskExecution.Status = TaskStatus.SUCCESSFUL;
                } catch (Exception exception) {
                    taskExecution.Log(exception);
                    taskExecution.Status = TaskStatus.FAILED;
                }
                _status = WorkerStatus.WAITING;
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
        }

        /// <summary>
        /// Cancels the worker execution when it's work is done.
        /// </summary>
        public void Cancel() {
            _canceled = true;
        }

        /// <summary>
        /// Abort a prior worker cancel.
        /// </summary>
        public void AbortCancel() {
            _canceled = false;
        }
    }
}