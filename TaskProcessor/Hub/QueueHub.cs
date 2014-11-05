using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Contract.Service;
using TaskProcessor.Contract.Queue;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hub {
    [Export(typeof(QueueHub))]
    [Export(typeof(IQueue))]
    [Export(typeof(IService))]
    public class QueueHub : IQueue {
        private readonly IQueueManager _queueManager;

        [Import]
        public QueueHub(IQueueManager queueManager) {
            _queueManager = queueManager;
        }

        public IEnumerable<ITaskQueue> GetAll() {
            return _queueManager.Queues;
        }

        public ITaskQueue GetByName(string name) {
            return _queueManager.Queues.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<ITaskQueue> FindByName(string name) {
            return _queueManager.Queues.Where(x => x.Name.Contains(name));
        }

        public ITaskQueue Create(string name) {
            return _queueManager.Create(name);
        }
    }
}
