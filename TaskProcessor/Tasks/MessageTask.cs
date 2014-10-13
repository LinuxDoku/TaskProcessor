using System;
using TaskProcessor.Contracts;
using System.Threading;
using TaskProcessor.DI.Attributes;
using TaskProcessor.DI;
using System.Linq;

namespace TaskProcessor.Tasks {
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
            Thread.CurrentThread.Join(new Random().Next(10000));
        }

        #endregion
    }
}