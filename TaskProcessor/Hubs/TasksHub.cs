using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Hubs;

namespace TaskProcessor.Hubs
{
    public class TasksHub : Hub, ITasksHub
    {
        private readonly ITaskManager _taskManager;

        public TasksHub() {
            
        }

        //[ImportingConstructor]
        //public TasksHub(ITaskManager taskManager) {
        //    _taskManager = taskManager;
        //}

        public IEnumerable<string> GetTasks() {
            return DI.GetExport<ITaskManager>().GetAll();
        }
    }
}

