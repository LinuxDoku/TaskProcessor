using System;
using TaskProcessor.Queue;
using System.IO;
using System.Reflection;
using Microsoft.Owin.Hosting;
using System.Net.Sockets;
using TaskProcessor.Configuration;
using TaskProcessor.Workers;

namespace TaskProcessor {
    class Program {
        public static void Main(string[] args) {
            var queue = new MemoryQueue();

            // try to read config
            var configFile = "./config.json";

            if (File.Exists(configFile)) {
                var configFileText = File.ReadAllText(configFile);
                var config = new JsonConfiguration(configFileText);

                if (config.Workers < 1) {
                    Console.WriteLine("Please add more than one worker to the config!");
                    return;
                }

                queue.Add(WorkerManager.Spawn(config.Workers));

                // add tasks to queue
                foreach (var task in config.Tasks) {
                    var taskExecution = new TaskExecution(task);
                    queue.Add(taskExecution);
                }

            } else {
                Console.WriteLine("No 'config.json' found!");
                return;
            }

            var host = "http://localhost:8080";
            try {
                using (WebApp.Start<Startup>(host)) {
                    Console.WriteLine("Server is running");
                    Console.ReadLine();
                }
            } catch (TargetInvocationException exception) {
                if (exception.InnerException.GetType() == typeof(SocketException)) {
                    Console.WriteLine("Port is already in use!");
                } else {
                    Console.WriteLine("Someting went wrong! " + exception.InnerException.Message);
                }
            }
        }
    }
}