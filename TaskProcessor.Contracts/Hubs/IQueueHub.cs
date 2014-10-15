using System.Collections.Generic;

namespace TaskProcessor.Contracts.Hubs {
    public interface IQueueHub {
        IEnumerable<ITaskQueue> GetAll();
        ITaskQueue GetByName(string name);
        IEnumerable<ITaskQueue> FindByName(string name);
        ITaskQueue Create(string name);
    }
}