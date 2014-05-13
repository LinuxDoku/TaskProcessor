using System;
using Microsoft.AspNet.SignalR.Client;
using System.Reflection.Emit;
using System.Net.Configuration;
using Microsoft.AspNet.SignalR.Client.Transports;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace TaskProcessor.Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:8080");
            IHubProxy hub = connection.CreateHubProxy("TasksHub");
            hub.On("SendMessage", msg => Console.WriteLine("test"));
            connection.Start().Wait();

            Thread.Sleep(1000);

            hub.Invoke("GetTasks").Wait();
        }
    }
}
