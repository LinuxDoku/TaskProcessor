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
        private IList<ITask> _tasks; 

        public JsonConfiguration(string jsonString)
        {
            var source = JObject.Parse(jsonString);
            if (source != null)
            {
                IsValid = true;

                Workers = (int) source.SelectToken("workers");
                _tasks = new List<ITask>();

                foreach (var taskConfig in source.SelectToken("tasks"))
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

                        if(constructor != null)
                        {
                            var parameters = constructor.GetParameters();
                            var arguments = new List<Object>();

                            for (var i = 0; i < parameters.Length; i++)
                            {
                                arguments.Add(argumentList[i].ToObject(parameters[i].ParameterType));
                            }

                            task = (ITask) Activator.CreateInstance(type, arguments.ToArray());
                            
                            _tasks.Add(task);
                        }
                    }
                    else
                    {
                        IsValid = false;
                    }
                }
            }
            else
            {
                IsValid = false;
            }
        }

        public bool IsValid { get; private set; }
        public int Workers { get; private set; }
        public IEnumerable<ITask> Tasks { get { return _tasks; } }
    }
}

