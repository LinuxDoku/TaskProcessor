using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Owin.Hosting;
using TaskProcessor.Configuration;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Configuration;
using TaskProcessor.DI.Attributes;
using TaskProcessor.Workers;
using System.Threading;

namespace TaskProcessor {
    [Export(typeof(IApplication))]
    [Shared]
    public class Application : IApplication, IDisposable {
        private readonly ITaskQueue _taskQueue;
        private readonly IWorkerManager _workerManager;
        private readonly IConfiguration _configuration;

        private readonly Thread _queueThread;
        private readonly Thread _signalrThread;

        [Import]
        public Application(ITaskQueue taskQueue, IWorkerManager workerManager, IConfiguration configuration) {
            _taskQueue = taskQueue;
            _workerManager = workerManager;
            _configuration = configuration;

            ParseConfig();

            _queueThread = new Thread(StartQueue);
            _signalrThread = new Thread(StartSignalr);

            _queueThread.Start();
            _signalrThread.Start();
        }

        public void Dispose() {
            _queueThread.Abort();
            _signalrThread.Abort();
        }

        private void ParseConfig() {
            // try to read config
            var configFile = "./config.json";

            if (File.Exists(configFile)) {
                var configFileText = File.ReadAllText(configFile);
                _configuration.Parse(configFileText);
            } else {
                Console.WriteLine("No 'config.json' found!");
            }
        }

        private void StartQueue() {
            if (_configuration.Workers < 1) {
                Console.WriteLine("Please add more than one worker to the config!");
                return;
            }

            _taskQueue.Add(_workerManager.Spawn(_configuration.Workers));

            // add tasks to queue
            foreach (var task in _configuration.Tasks) {
                var taskExecution = new TaskExecution(task);
                _taskQueue.Add(taskExecution);
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
