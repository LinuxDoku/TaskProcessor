using System;
using Microsoft.AspNet.SignalR;

namespace TaskProcessor.Hubs
{
    public class TasksHub : Hub
    {
        public void GetTasks() 
        {
            Console.WriteLine("invoke");
            Clients.All.SendMessage("Hello World");
        }
    }
}

