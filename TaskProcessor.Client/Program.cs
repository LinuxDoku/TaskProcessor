using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using TaskProcessor.Contracts;

namespace TaskProcessor.Client
{
    class MainClass 
    {
        public static void Main(string[] args) {
            var task = new Task(Client);
            task.Start();
            task.Wait();

            Console.ReadLine();
        }

        private class TaskQueue {
            public string Name { get; set; }
        }

        private static async void Client() {
            var connection = new HubConnection("http://localhost:8080");
            IHubProxy hub = connection.CreateHubProxy("QueueHub");
            await connection.Start();

            var queues = await hub.Invoke<IEnumerable<TaskQueue>>("GetAll");
            foreach (var queue in queues) {
                Console.WriteLine(queue.Name);
            }

            connection.Stop();
        }
    }
}
