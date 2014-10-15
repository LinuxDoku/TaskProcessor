using System.Collections.Generic;
using TaskProcessor.Contract.Task;

namespace TaskProcessor.Contract.Hub {
    public interface ITasksHub {
        IEnumerable<ITask> GetTasks();
    }
}
