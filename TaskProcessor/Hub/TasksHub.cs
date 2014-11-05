using System.Collections.Generic;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Service;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hub {
    [Export(typeof(ITasks))]
    [Export(typeof(IService))]
    public class TasksHub :  ITasks {
        private readonly ITaskManager _taskManager;

        [Import]
        public TasksHub(ITaskManager taskManager) {
            _taskManager = taskManager;
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITask> GetTasks() {
            //return _taskManager.GetAll();
            return null;
        }

        public void Schedule(ITaskExecution taskExecution) {
            
        }
    }
}