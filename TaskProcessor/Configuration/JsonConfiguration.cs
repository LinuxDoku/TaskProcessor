using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contracts;

namespace TaskProcessor.Configuration
{
    public class JsonConfiguration : IConfiguration
    {
        private int _workers;
        private IList<ITask> _tasks;

        public JsonConfiguration(string jsonString)
        {
            var source = JObject.Parse(jsonString);
            if (source != null) { 
                _workers = (int)source.SelectToken("workers");
                _tasks = new List<ITask>();

                foreach (var taskConfig in source.SelectToken("tasks"))
                {
                    ITask task = null;

                    var assemblyName = (string)taskConfig.SelectToken("assembly");
                    var className = (string)taskConfig.SelectToken("class");
                    var argumentList = taskConfig.SelectToken("arguments");

                    var typeName = className;
                    if (assemblyName != null)
                    {
                        typeName = className + "," + assemblyName;
                    }

                    var type = Type.GetType(typeName);
                    foreach (var constructor in type.GetConstructors())
                    {
                        var parameters = constructor.GetParameters();
                        if (parameters.Length == argumentList.Count())
                        {
                            // try with this constructor
                            try
                            {
                                var arguments = new List<Object>();

                                for (var i = 0; i < parameters.Length; i++)
                                {
                                    arguments.Add(argumentList[i].ToObject(parameters[i].ParameterType));
                                }

                                task = (ITask)Activator.CreateInstance(type, arguments.ToArray());
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        _tasks.Add(task);
                    }
                }
            }
        }

        #region IConfiguration implementation

        public int Workers
        {
            get { return _workers; }
        }

        public IEnumerable<ITask> Tasks
        {
            get { return _tasks; }
        }

        #endregion
    }
}

