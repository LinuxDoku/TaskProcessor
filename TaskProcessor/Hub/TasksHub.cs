using System.Collections.Generic;
using TaskProcessor.Contract.Hub;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hub {
    [Export(typeof (TasksHub))]
    public class TasksHub : Microsoft.AspNet.SignalR.Hub, ITasksHub {
        private readonly ITaskManager _taskManager;

        [Import]
        public TasksHub(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITask<ITaskConfiguration>> GetTasks() {
            //return _taskManager.GetAll();
            return null;
        }

        public void Schedule(ITaskExecution taskExecution) {
            
        }
    }
}