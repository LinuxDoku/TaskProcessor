using System;
using System.Collections.Generic;
using System.Composition;
using TaskProcessor.Contracts;

namespace TaskProcessor.Workers {
    [Export(typeof(IWorkerManager))]
    public class WorkerManager : IWorkerManager {
        public IEnumerable<IWorker> Spawn(int numerOfWorkers) {
            var workers = new List<IWorker>();

            for (var i = 0; i <= numerOfWorkers; i++) {
                workers.Add(DI.GetExport<IWorker>());
            }

            return workers;
        }
    }

    public interface IWorkerManager {
        IEnumerable<IWorker> Spawn(int numerOfWorkers);
    }
}
