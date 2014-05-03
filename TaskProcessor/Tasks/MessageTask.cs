using System;
using TaskProcessor.Contracts;

namespace TaskProcessor.Tasks
{
	public class MessageTask : ITask
	{
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