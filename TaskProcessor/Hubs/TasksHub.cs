using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Hubs;

namespace TaskProcessor.Hubs
{
    [HubName("TasksHub")]
    public class TasksHub : Hub, ITasksHub
    {
        public IEnumerable<string> GetTasks() {
            return DI.GetExport<ITaskManager>().GetAll();
        }
    }
}

