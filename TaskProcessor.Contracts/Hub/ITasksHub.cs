using System.Collections.Generic;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contracts;

namespace TaskProcessor.Contract.Hub {
    public interface ITasksHub {
        IEnumerable<ITask> GetTasks();
    }
}
