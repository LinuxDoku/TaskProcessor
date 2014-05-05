using System;
using TaskProcessor.Contracts;
using System.Threading;

namespace TaskProcessor.Tasks
{
	/// <summary>
	/// A simple task which prints a message to stdout.
	/// </summary>
	public class MessageTask : ITask
	{
		public MessageTask(string message) {
			Message = message;
		}

		/// <summary>
		/// The message to print.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

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
		/// Execute this task. And change the task status if it was successful.
		/// </summary>
		public void Execute()
		{
			Console.WriteLine(Message);
			Thread.CurrentThread.Join(new Random().Next(10000));

			TaskStatus = TaskStatus.SUCCESSFUL;
		}

		/// <summary>
		/// Get the current task status.
		/// </summary>
		/// <value>The task status.</value>
		public TaskStatus TaskStatus { get; set; }

		#endregion
	}
}