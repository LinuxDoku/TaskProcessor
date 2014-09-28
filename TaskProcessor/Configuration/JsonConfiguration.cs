using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Configuration;

namespace TaskProcessor.Configuration
{
    public class JsonConfiguration : IConfiguration
    {
        private readonly IList<ITask> _tasks; 

        public JsonConfiguration(string jsonString)
        {
            JObject source = null;
            try {
                source = JObject.Parse(jsonString);
            } catch (Exception ex) {
                throw new Exception("Invalid configuration file!", ex);
            }

            if (source != null)
            {
                var workers = source.SelectToken("workers");
                var tasks = source.SelectToken("tasks");

                // workers
                if (workers != null)
                {
                    Workers = (int) workers;
                }

                // tasks
                _tasks = new List<ITask>();
                if (tasks != null && tasks.HasValues)
                {
                    foreach (var taskConfig in tasks)
                    {
                        // TODO: move this higher logic to TaskLoader
                        ITask task = null;

                        var assemblyName = (string) taskConfig.SelectToken("assembly");
                        var className = (string) taskConfig.SelectToken("class");
                        var argumentList = taskConfig.SelectToken("arguments");

                        var typeName = className;
                        if (assemblyName != null)
                        {
                            typeName = className + "," + assemblyName;
                        }

                        var type = Type.GetType(typeName);
                        if (type != null)
                        {
                            var constructor = type.GetConstructors()
                                .FirstOrDefault(x => x.GetParameters().Length == argumentList.Count());

                            if (constructor != null)
                            {
                                var args =
                                    constructor.GetParameters()
                                        .Select((t, i) => argumentList[i].ToObject(t.ParameterType));

                                try
                                {
                                    task = (ITask) Activator.CreateInstance(type, args.ToArray());
                                }
                                catch (Exception ex)
                                {
                                }

                                if (task != null)
                                {
                                    _tasks.Add(task);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int Workers { get; private set; }
        public IEnumerable<ITask> Tasks { get { return _tasks; } }
    }
}

