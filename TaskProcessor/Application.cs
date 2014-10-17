using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Microsoft.Owin.Hosting;
using TaskProcessor.Contract.Configuration;
using TaskProcessor.Contract.Queue;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;
using TaskProcessor.Task;
using TaskProcessor.Worker;
using System.Threading;

namespace TaskProcessor {
    [Export(typeof(IApplication))]
    [Shared]
    public class Application : IApplication, IDisposable {
        private readonly IQueueManager _queueManager;
        private readonly IWorkerManager _workerManager;
        private readonly IConfiguration _configuration;
        private readonly ITaskManager _taskManager;

        private readonly Thread _queueThread;
        private readonly Thread _signalrThread;

        [Import]
        public Application(IQueueManager queueManager, IWorkerManager workerManager, ITaskManager taskManager, IConfiguration configuration) {
            _queueManager = queueManager;
            _workerManager = workerManager;
            _taskManager = taskManager;
            _configuration = configuration;

            if (ParseConfig()) {
                _queueThread = new Thread(StartQueue);
                _signalrThread = new Thread(StartSignalr);

                _queueThread.Start();
                _signalrThread.Start();
            }
        }

        public void Dispose() {
            _queueThread.Abort();
            _signalrThread.Abort();
        }

        private bool ParseConfig() {
            // try to read config
            var configFile = "./config.json";

            if (File.Exists(configFile)) {
                var configFileText = File.ReadAllText(configFile);
                _configuration.Parse(configFileText);

                return true;
            }
            
            Console.WriteLine("No 'config.json' found!");
            return false;
        }

        private void StartQueue() {
            if (_configuration.Workers < 1) {
                Console.WriteLine("Please add more than one worker to the config!");
                return;
            }
            
            var queue = _queueManager.Create();
            queue.Add(_workerManager.Spawn(_configuration.Workers));

            // register tasks
            foreach (var task in _configuration.Tasks) {
                _taskManager.Register(task);
                queue.Add(new TaskExecution(task));
            }
        }

        private void StartSignalr() {
            // TODO: create own url builder
            var hostBuilder = new StringBuilder();
            hostBuilder.Append(_configuration.UseHttps ? "https" : "http")
                       .Append("://")
                       .Append(_configuration.Hostname)
                       .Append(":")
                       .Append(_configuration.Port);

            try {
                using (WebApp.Start<OwinStartup>(hostBuilder.ToString())) {
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
