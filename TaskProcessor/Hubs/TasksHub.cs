using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Hubs;
using System.Linq;
using System;

namespace TaskProcessor.Hubs
{
    [HubName("TasksHub")]
    public class TasksHub : Hub
    {
        public IEnumerable<string> GetTasks()
        {
            var result =  DI.GetExport<ITaskManager>().GetAll();

            return result;
        }
    }
}

