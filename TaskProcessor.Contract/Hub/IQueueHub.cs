using System.Collections.Generic;
using TaskProcessor.Contract.Queue;

namespace TaskProcessor.Contract.Hub {
    public interface IQueueHub {
        IEnumerable<ITaskQueue> GetAll();
        ITaskQueue GetByName(string name);
        IEnumerable<ITaskQueue> FindByName(string name);
        ITaskQueue Create(string name);
    }
}