using System.Collections;
using System.Collections.Generic;
using TaskProcessor.Contract.Queue;

namespace TaskProcessor.Contracts.Queue {
    public interface IQueueManager {
        IEnumerable<ITaskQueue> Queues { get; }
        ITaskQueue Create();
        ITaskQueue Create(string name);
    }
}
