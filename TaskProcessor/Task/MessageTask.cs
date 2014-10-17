using System;
using System.Linq;
using System.Threading;
using TaskProcessor.Contract.Queue;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Task {
    /// <summary>
    /// A simple task which prints a message to stdout.
    /// </summary>
    [Export(typeof(ITask))]
    public class MessageTask : ITask {
        public MessageTask(string message) {
            Message = message;
        }

        /// <summary>
        /// The message to print.
        /// </summary>
        /// <value>The message.</value>
        private string Message { get; set; }

        #region ITask implementation

        /// <summary>
        /// Get the task's name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get {
                return "MessageTask";
            }
        }

        /// <summary>
        /// Execute this task.
        /// </summary>
        public void Execute() {
            Console.WriteLine(Message);

            var queueManager = Container.GetExport<IQueueManager>();
            var taskManager = Container.GetExport<ITaskManager>();
            var queue = queueManager.Queues.FirstOrDefault();

            if (queue != null) {
                queue.Add(taskManager.Create("MessageTask", DateTime.Now.AddSeconds(30), DateTime.Now.ToString()));
            }
        }

        #endregion
    }
}