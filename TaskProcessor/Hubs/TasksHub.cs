using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Hubs;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hubs
{
    [HubName("TasksHub")]
    [Export(typeof(TasksHub))]
    //[Shared]
    public class TasksHub : Hub, ITasksHub
    {
        private readonly ITaskManager _taskManager;

        [Import]
        public TasksHub(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        public IEnumerable<string> GetTasks()
        {
            //var result = _taskManager.GetAll();
            var result = new List<string>() {"test"};
            return result;
        }
    }
}

