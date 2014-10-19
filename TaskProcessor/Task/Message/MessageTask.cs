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

        public void Execute(MessageTaskConfiguration configuration) {
            Console.WriteLine(configuration.Message);
        }

        #endregion
    }
}