using System.Collections.Generic;

namespace TaskProcessor.Contracts
{
	/// <summary>
	/// A queue which a set of tasks which have to be executed.
	/// </summary>
	public interface ITaskQueue
	{
		/// <summary>
		/// Add a task to the queue.
		/// </summary>
		/// <param name="task">Task.</param>
		void Add(ITask task);

		/// <summary>
		/// Get the next task in the queue.
		/// </summary>
		/// <returns>The next.</returns>
		ITask GetNext();

		/// <summary>
		/// Get all queued tasks.
		/// </summary>
		/// <returns>List of all queued tasks.<returns>
		List<ITask> GetAll();
	}
}