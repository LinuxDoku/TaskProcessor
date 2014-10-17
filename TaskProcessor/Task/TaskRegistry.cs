using System;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Task {
    [Export(typeof(ITaskRegistry))]
    public class TaskRegistry : ITaskRegistry {
        private readonly IDictionary<string, Type> _tasks;

        public IDictionary<string, Type> Tasks {
            get { return _tasks; }
        }

        public TaskRegistry() {
            _tasks = new Dictionary<string, Type>();
        }

        public void Register(string typeName) {
            Register(Type.GetType(typeName));
        }

        public void Register(ITask task) {
            if(_tasks.All(x => x.Key != task.Name)) {
                _tasks.Add(task.Name, task.GetType());
            }
        }

        public void Register(Type taskType) {
            if (taskType != null) {
                var task = (ITask)Activator.CreateInstance(taskType);
                Register(task);
            }
        }

        public void Delete(string taskName) {
            if (_tasks.ContainsKey(taskName)) {
                _tasks.Remove(taskName);
            }
        }

        public void Delete(ITask task) {
            if (task != null) {
                _tasks.Remove(_tasks.FirstOrDefault());
            }
        }

        public void Delete(Type taskType) {
            var task = _tasks.FirstOrDefault(x => x.Value.Name == taskType.Name);
            if (string.IsNullOrEmpty(task.Key)) {
                _tasks.Remove(task);
            }
        }
    }
}
