using System.Collections.Generic;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Queue;

namespace TaskProcessor.Contract.Service {
    public interface IQueue : IService {
        IEnumerable<ITaskQueue> GetAll();
        ITaskQueue GetByName(string name);
        IEnumerable<ITaskQueue> FindByName(string name);
        ITaskQueue Create(string name);
    }
}