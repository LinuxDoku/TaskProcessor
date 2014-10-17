using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contract.Configuration;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Configuration {
    [Export(typeof(IConfiguration))]
    public class JsonConfiguration : IConfiguration {
        private IList<ITask> _tasks;
        private readonly ITaskManager _taskManager;

        [Import]
        public JsonConfiguration(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        public void Parse(string jsonString) {
            JObject source = null;
            try {
                source = JObject.Parse(jsonString);
            } catch (Exception ex) {
                throw new Exception("Invalid configuration file!", ex);
            }

            if (source != null) {
                var workers = source.SelectToken("workers");
                var tasks = source.SelectToken("tasks");

                // workers
                if (workers != null) {
                    Workers = (int)workers;
                }

                // tasks
                _tasks = new List<ITask>();
                if (tasks != null && tasks.HasValues) {
                    foreach (var taskConfig in tasks) {
                        // TODO: move this higher logic to TaskLoader
                        ITask task = null;

                        var assemblyName = (string)taskConfig.SelectToken("assembly");
                        var className = (string)taskConfig.SelectToken("class");
                        var argumentList = taskConfig.SelectToken("arguments");

                        var typeName = className;
                        if (assemblyName != null) {
                            typeName = className + "," + assemblyName;
                        }

                        var type = Type.GetType(typeName);
                        if (type != null) {
                            var constructor = type.GetConstructors()
                                .FirstOrDefault(x => x.GetParameters().Length == argumentList.Count());

                            if (constructor != null) {
                                var args =
                                    constructor.GetParameters()
                                        .Select((t, i) => argumentList[i].ToObject(t.ParameterType));

                                try {
                                    task = (ITask) Activator.CreateInstance(type, args.ToArray());
                                }
                                catch (Exception) {
                                }

                                if (task != null) {
                                    _tasks.Add(task);
                                }
                            }
                        }
                        else {
                            Console.WriteLine("Task {0} not found!", typeName);
                        }
                    }
                }

                // communication
                var communication = source.SelectToken("communication");

                if (communication != null && communication.HasValues) {
                    UseHttps = (bool)communication.SelectToken("useHttps");
                    Hostname = (string) communication.SelectToken("hostname");
                    Port = (short) communication.SelectToken("port");
                }
            }
        }

        public int Workers { get; private set; }
        public IEnumerable<ITask> Tasks { get { return _tasks; } }
        public bool UseHttps { get; private set; }
        public string Hostname { get; private set; }
        public short Port { get; private set; }
    }
}

