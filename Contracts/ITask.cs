namespace TaskProcessor.Contracts
{
	/// <summary>
	/// A task is a job which can be executed and does whatever the developer want's.
	/// </summary>
	public interface ITask
	{
		/// <summary>
		/// Get the task's name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// Execute this task. And change the task status if it was successful.
		/// </summary>
		void Execute();

		/// <summary>
		/// Get the current task status.
		/// </summary>
		/// <value>The task status.</value>
		TaskStatus TaskStatus { get; set; }
	}
}