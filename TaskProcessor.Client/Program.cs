using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;

namespace TaskProcessor.Client
{
    class MainClass
    {
        public static void Main(string[] args) {
            Client();
        }

        private static async void Client() {
            var connection = new HubConnection("http://localhost:8080");
            IHubProxy hub = connection.CreateHubProxy("TasksHub");
            await connection.Start();

            var results = await hub.Invoke<IEnumerable<string>>("GetTasks");

            foreach (var res in results) {
                Console.WriteLine(res);
            }

            Console.ReadLine();
        }
    }
}
