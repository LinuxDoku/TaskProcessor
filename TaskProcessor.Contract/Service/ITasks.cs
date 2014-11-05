using System.Collections.Generic;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Task;

namespace TaskProcessor.Contract.Service {
    public interface ITasks : IService {
        IEnumerable<ITask> GetTasks();
    }
}
