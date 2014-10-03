using System;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using TaskProcessor.Tasks;

namespace TaskProcessor.Hubs
{
    public class TasksHub : Hub
    {
        private readonly ITaskManager _taskManager;

        public TasksHub(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        public void GetTasks() {
            var tasks = _taskManager.GetAll();
            Clients.Caller.SendMessage(JsonConvert.SerializeObject(tasks));
        }
    }
}

