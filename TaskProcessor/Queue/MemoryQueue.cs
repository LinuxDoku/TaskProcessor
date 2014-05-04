using TaskProcessor.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace TaskProcessor.Queue
{
	/// <summary>
	/// In memory task queue.
	/// </summary>
	public class MemoryQueue : ITaskQueue
	{
		protected List<ITask> Tasks;

		public MemoryQueue()
		{
			Tasks = new List<ITask>();
		}

		#region ITaskList implementation

		/// <summary>
		/// Add a task to the queue.
		/// </summary>
		/// <param name="task">Task.</param>
		public void Add(ITask task)
		{
			Tasks.Add(task);
			task.TaskStatus = TaskStatus.QUEUED;
		}

		/// <summary>
		/// Get the next task in the queue.
		/// </summary>
		/// <returns>The next.</returns>
		public ITask GetNext()
		{
			var suiteable = Tasks.FirstOrDefault(x => x.TaskStatus == TaskStatus.QUEUED);
			if(suiteable != null) {
				suiteable.TaskStatus = TaskStatus.RUNNING;
			}
			return suiteable;
		}

		/// <summary>
		/// Get all queued tasks.
		/// </summary>
		/// <returns>The all.</returns>
		public List<ITask> GetAll()
		{
			return Tasks;
		}

		#endregion
	}
}