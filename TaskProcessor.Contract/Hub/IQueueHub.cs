using System.Collections.Generic;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Queue;

namespace TaskProcessor.Contract.Hub {
    public interface IQueueHub : IService {
        IEnumerable<ITaskQueue> GetAll();
        ITaskQueue GetByName(string name);
        IEnumerable<ITaskQueue> FindByName(string name);
        ITaskQueue Create(string name);
    }
}