using System;
using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contracts;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Task {
    [Export(typeof(ITaskManager))]
    [Shared]
    public class TaskManager : ITaskManager {
        private readonly ITaskRegistry _taskRegistry;

        [Import]
        public TaskManager(ITaskRegistry taskRegistry) {
            _taskRegistry = taskRegistry;
        }

        public void Register(string typeName) {
            _taskRegistry.Register(typeName);
        }

        public void Register(ITask task) {
            _taskRegistry.Register(task);
        }

        public ITaskExecution Create(string taskName, object parameter=null) {
            return Create(taskName, DateTime.Now, parameter);
        }

        public ITaskExecution Create(string taskName, DateTime dateTime, object parameter=null) {
            var task = _taskRegistry.Tasks.FirstOrDefault(x => x.Name == taskName);

            if (task != null) {
                return new TaskExecution(task, dateTime);
            }

            return null;
        }

        public IEnumerable<ITask> GetAll() {
            return _taskRegistry.Tasks;
        }
    }
}
