using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using Microsoft.Owin.Hosting;
using TaskProcessor.Configuration;
using TaskProcessor.Contracts;
using TaskProcessor.DI.Attributes;
using TaskProcessor.Workers;
using System.Threading;

namespace TaskProcessor {
    [Export(typeof(IApplication))]
    [Shared]
    public class Application : IApplication, IDisposable {
        private readonly ITaskQueue _taskQueue;
        private readonly IWorkerManager _workerManager;

        private readonly Thread _queueThread;
        private readonly Thread _signalrThread;

        [Import]
        public Application(ITaskQueue taskQueue, IWorkerManager workerManager) {
            _taskQueue = taskQueue;
            _workerManager = workerManager;

            _queueThread = new Thread(StartQueue);
            _signalrThread = new Thread(StartSignalr);

            _queueThread.Start();
            _signalrThread.Start();
        }

        public void Dispose() {
            _queueThread.Abort();
            _signalrThread.Abort();
        }

        private void StartQueue() {
            // try to read config
            var configFile = "./config.json";

            if (File.Exists(configFile)) {
                var configFileText = File.ReadAllText(configFile);
                var config = new JsonConfiguration(configFileText);

                if (config.Workers < 1) {
                    Console.WriteLine("Please add more than one worker to the config!");
                    return;
                }

                _taskQueue.Add(_workerManager.Spawn(config.Workers));

                // add tasks to queue
                foreach (var task in config.Tasks) {
                    var taskExecution = new TaskExecution(task);
                    _taskQueue.Add(taskExecution);
                }

            } else {
                Console.WriteLine("No 'config.json' found!");
            }
        }

        private void StartSignalr() {
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
