using System;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contracts;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Tasks {
    [Export(typeof(ITaskManager))]
    [Shared]
    public class TaskManager : ITaskManager {
        private IDictionary<string, Type> Registry { get; set; }

        public TaskManager() {
            Registry = new Dictionary<string, Type>();
        }

        public bool RegisterTask(string taskName, string typeName) {
            var type = Type.GetType(typeName);

            if (type != null) {
                return RegisterTask(taskName, type);
            }

            return false;
        }

        public bool RegisterTask(string taskName, Type type) {
            if (!Registry.ContainsKey(taskName)) {
                Registry.Add(taskName, type);
            } else {
                Registry[taskName] = type;
            }

            return true;
        }

        public ITask Create(string taskName, object parameter = null) {
            if (Registry.ContainsKey(taskName)) {
                var type = Registry[taskName];
                return (ITask)Activator.CreateInstance(type, parameter);
            }

            return null;
        }

        public IEnumerable<string> GetAll() {
            return Registry.Select(x => x.Key);
        }
    }
}
