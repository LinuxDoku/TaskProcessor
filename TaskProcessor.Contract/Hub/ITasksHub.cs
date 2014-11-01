using System.Collections.Generic;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Task;

namespace TaskProcessor.Contract.Hub {
    public interface ITasksHub : IService {
        IEnumerable<ITask> GetTasks();
    }
}
