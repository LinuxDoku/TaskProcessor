using System.Collections.Generic;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Queue;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Queue {
    [Export(typeof(IQueueManager))]
    public class QueueManager : IQueueManager {
        private readonly IList<ITaskQueue> _queue; 

        public QueueManager() {
            _queue = new List<ITaskQueue>();
        }

        public IEnumerable<ITaskQueue> Queues {
            get { return _queue; }
        }

        public ITaskQueue Create() {
            var queue = Container.GetExport<ITaskQueue>();
            _queue.Add(queue);

            return queue;
        }
    }
}
