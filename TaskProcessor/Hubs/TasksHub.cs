using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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
        public IEnumerable<string> GetTasks() {
            return _taskManager.GetAll();
        }
    }
}