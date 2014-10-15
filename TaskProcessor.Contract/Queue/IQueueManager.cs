using System.Collections.Generic;

namespace TaskProcessor.Contract.Queue {
    public interface IQueueManager {
        IEnumerable<ITaskQueue> Queues { get; }
        ITaskQueue Create();
        ITaskQueue Create(string name);
    }
}
