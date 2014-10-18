using System;
using System.Linq;
using TaskProcessor.Contract.Queue;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Task.Message {
    /// <summary>
    /// A simple task which prints a message to stdout.
    /// </summary>
    [Export(typeof(ITask<ITaskConfiguration>))]
    public class MessageTask : ITask<MessageTaskConfiguration> {

        #region ITask implementation

        /// <summary>
        /// Get the task's name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get {
                return "TaskProcessor.Task.Message.MessageTask";
            }
        }

        public void Execute(ITaskConfiguration configuration) {
            Execute((MessageTaskConfiguration)configuration);
        }

        /// <summary>
        /// Execute this task.
        /// </summary>
        public void Execute() {
            Console.WriteLine("");

            var queueManager = Container.GetExport<IQueueManager>();
            var taskManager = Container.GetExport<ITaskManager>();
            var queue = queueManager.Queues.FirstOrDefault();

            if (queue != null) {
                queue.Add(taskManager.Create("MessageTask", DateTime.Now.AddSeconds(30), new MessageTaskConfiguration() {
                    Message = DateTime.Now.ToString()
                }));
            }
        }

        public void Execute(MessageTaskConfiguration configuration) {
            Console.WriteLine(configuration.Message);
        }

        #endregion
    }
}