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
        private IDictionary<string, IList<ITaskConfiguration>> _tasks;

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
                _tasks = new Dictionary<string, IList<ITaskConfiguration>>();
                if (tasks != null && tasks.HasValues) {
                    foreach (var taskConfig in tasks) {
                        // TODO: move this higher logic to TaskLoader

                        var assemblyName = (string)taskConfig.SelectToken("assembly");
                        var className = (string)taskConfig.SelectToken("class");
                        var taskConfiguration = taskConfig.SelectToken("configuration");

                        var typeName = className;
                        if (assemblyName != null) {
                            typeName = className + "," + assemblyName;
                        }

                        var type = Type.GetType(typeName);
                        if (type != null) {
                            var configurationTypeName = type.GetInterface(typeof(ITask<ITaskConfiguration>).Name).GetGenericArguments().FirstOrDefault().FullName;
                            var configurationType = Type.GetType(configurationTypeName);
                            var configuration = (ITaskConfiguration)taskConfiguration.ToObject(configurationType);

                            if (_tasks.ContainsKey(type.FullName)) {
                                _tasks[type.FullName].Add(configuration);
                            } else {
                                var list = new List<ITaskConfiguration> { configuration };
                                _tasks.Add(type.FullName, list);
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
                    Https = (bool)communication.SelectToken("https");
                    Hostname = (string)communication.SelectToken("hostname");
                    Port = (short)communication.SelectToken("port");
                }
            }
        }

        public int Workers { get; private set; }
        public IDictionary<string, IList<ITaskConfiguration>> Tasks { get { return _tasks; } }
        public bool Https { get; private set; }
        public string Hostname { get; private set; }
        public short Port { get; private set; }
    }
}

