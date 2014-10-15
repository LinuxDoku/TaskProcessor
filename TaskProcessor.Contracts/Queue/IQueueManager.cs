using System.Collections;
using System.Collections.Generic;

namespace TaskProcessor.Contracts.Queue {
    public interface IQueueManager {
        IEnumerable<ITaskQueue> Queues { get; }
        ITaskQueue Create();
    }
}
