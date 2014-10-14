using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Hubs;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hubs {
    [Export(typeof (TasksHub))]
    public class TasksHub : Hub, ITasksHub {
        private readonly ITaskManager _taskManager;

        [Import]
        public TasksHub(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITask> GetTasks() {
            return _taskManager.GetAll();
        }

        public void Schedule(ITaskExecution taskExecution) {
            
        }
    }
}