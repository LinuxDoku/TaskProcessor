using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Microsoft.Owin.Hosting;
using TaskProcessor.Communication;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Configuration;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;
using System.Threading;
using TaskProcessor.Contract.Worker;
using TaskProcessor.Contract.Queue;

namespace TaskProcessor {
    [Export(typeof(IApplication))]
    [Shared]
    public class Application : IApplication, IDisposable {
        private readonly IQueueManager _queueManager;
        private readonly IWorkerManager _workerManager;
        private readonly IConfiguration _configuration;
        private readonly ITaskManager _taskManager;
        private readonly IServer _server;


        private Thread _queueThread;
        private Thread _signalrThread;

        [Import]
        public Application() {
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IServer)));
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(Server)));
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(Communication.Infrastructure.Server)));

            _queueManager = DI.Container.GetExport<IQueueManager>();
            _workerManager = DI.Container.GetExport<IWorkerManager>();
            _configuration = DI.Container.GetExport<IConfiguration>();
            _taskManager = DI.Container.GetExport<ITaskManager>();
            _server = DI.Container.GetExport<IServer>();
        }


        public void Run() {
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
                _taskManager.Register(task.Key);

                foreach (var configuration in task.Value) {
                    queue.Add(_taskManager.Create(task.Key, configuration));
                }
            }
        }

        private void StartSignalr() {
            // TODO: create own url builder
            var hostBuilder = new StringBuilder();
            hostBuilder.Append(_configuration.Https ? "https" : "http")
                       .Append("://")
                       .Append(_configuration.Hostname)
                       .Append(":")
                       .Append(_configuration.Port);

            try {
                using (WebApp.Start(hostBuilder.ToString(), _server.Start)) {
                    Console.WriteLine("Server is started");
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
