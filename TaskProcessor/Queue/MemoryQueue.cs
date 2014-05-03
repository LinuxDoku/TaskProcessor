using TaskProcessor.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace TaskProcessor.Queue
{
	public class MemoryQueue : ITaskQueue
	{
		protected List<ITask> Tasks;

		public MemoryQueue()
		{
			Tasks = new List<ITask>();
		}

		#region ITaskList implementation

		public void Add(ITask task)
		{
			Tasks.Add(task);
			task.TaskStatus = TaskStatus.QUEUED;
		}

		public ITask GetNext()
		{
			var suiteable = Tasks.FirstOrDefault(x => x.TaskStatus == TaskStatus.QUEUED);
			if(suiteable != null) {
				suiteable.TaskStatus = TaskStatus.RUNNING;
			}
			return suiteable;
		}
			
		public List<ITask> GetAll()
		{
			return Tasks;
		}

		#endregion
	}
}