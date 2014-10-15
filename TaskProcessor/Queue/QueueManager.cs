using System;
using System.Collections.Generic;
using TaskProcessor.Contract.Queue;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Queue {
    [Export(typeof(IQueueManager))]
    [Shared]
    public class QueueManager : IQueueManager {
        private readonly IList<ITaskQueue> _queue; 

        public QueueManager() {
            _queue = new List<ITaskQueue>();
        }

        public IEnumerable<ITaskQueue> Queues {
            get { return _queue; }
        }

        public ITaskQueue Create() {
            return Create(Guid.NewGuid().ToString());
        }

        public ITaskQueue Create(string name) {
            var queue = Container.GetExport<ITaskQueue>();
            queue.Name = name;

            _queue.Add(queue);

            return queue;
        }
    }
}
