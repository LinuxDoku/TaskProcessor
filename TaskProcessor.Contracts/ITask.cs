namespace TaskProcessor.Contracts
{
	/// <summary>
	/// A task is a job which can be executed and does whatever the developer want's.
	/// </summary>
	public interface ITask
	{
		/// <summary>
        /// A tasks unique identifier.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// Execute this task.
		/// </summary>
		void Execute();
	}
}