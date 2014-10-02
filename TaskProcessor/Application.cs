using System;
using System.Composition;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using Microsoft.Owin.Hosting;
using TaskProcessor.Configuration;
using TaskProcessor.Contracts;
using TaskProcessor.Workers;

namespace TaskProcessor {
    [Export(typeof(IApplication))]
    [Shared]
    public class Application : IApplication {
        [ImportingConstructor]
        public Application(ITaskQueue taskQueue, IWorkerManager workerManager) {
            // try to read config
            var configFile = "./config.json";

            if (File.Exists(configFile)) {
                var configFileText = File.ReadAllText(configFile);
                var config = new JsonConfiguration(configFileText);

                if (config.Workers < 1) {
                    Console.WriteLine("Please add more than one worker to the config!");
                    return;
                }

                taskQueue.Add(workerManager.Spawn(config.Workers));

                // add tasks to queue
                foreach (var task in config.Tasks) {
                    var taskExecution = new TaskExecution(task);
                    taskQueue.Add(taskExecution);
                }

            } else {
                Console.WriteLine("No 'config.json' found!");
                return;
            }

            var host = "http://localhost:8080";
            try {
                using (WebApp.Start<OwinStartup>(host)) {
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
