using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Tasks;
using TaskProcessor.Contracts;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Tasks {
    [Export(typeof(ITaskRegistry))]
    public class TaskRegistry : ITaskRegistry {
        private readonly IList<ITask> _taskList;

        public IEnumerable<ITask> Tasks {
            get { return _taskList; }
        }

        public TaskRegistry() {
            _taskList = new List<ITask>();
        }

        public void Register(string typeName) {
            var type = Type.GetType(typeName);

            if (type != null) {
                var task = (ITask)Activator.CreateInstance(type);
                Register(task);
            }
        }

        public void Register(ITask task) {
            if(_taskList.All(x => x.Name != task.Name)) {
                _taskList.Add(task);
            }
        }

        public void Delete(string taskName) {
            Delete(_taskList.FirstOrDefault(x => x.Name == taskName));
        }

        public void Delete(ITask task) {
            if (task != null) {
                _taskList.Remove(task);
            }
        }
    }
}
