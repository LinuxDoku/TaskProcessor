using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;

namespace TaskProcessor.Client
{
    class MainClass
    {
        public static void Main(string[] args) {
            Client();
        }

        private static void Client() {
            var connection = new HubConnection("http://localhost:8080");
            IHubProxy hub = connection.CreateHubProxy("TasksHub");
            connection.Start().Wait();

            Thread.Sleep(1000);

            hub.Invoke<IEnumerable<string>>("GetTasks").ContinueWith(task => {
                if (task.IsFaulted) {
                    Console.WriteLine(task.Exception.Message);
                    return;
                }

                foreach (var result in task.Result) {
                    Console.WriteLine(result);
                }
            }).Wait();

            connection.Stop();

            Console.ReadLine();
        }
    }
}
