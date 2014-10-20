namespace TaskProcessor.Contract.Task
{
    public interface ITask 
    {
        string Name { get; }
        void Execute(ITaskConfiguration configuration);
    }

	/// <summary>
	/// A task is a job which can be executed and does whatever the developer want's.
	/// </summary>
	public interface ITask<T> : ITask where T : ITaskConfiguration
	{
		/// <summary>
        /// A tasks unique identifier.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// Execute this task.
		/// </summary>
		void Execute(T configuration);
	}
}