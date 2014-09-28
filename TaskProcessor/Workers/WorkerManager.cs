using System.Collections.Generic;
using TaskProcessor.Contracts;

namespace TaskProcessor.Workers {
    public class WorkerManager {
        public static IEnumerable<IWorker> Spawn(int numerOfWorkers) {
            var workers = new List<IWorker>();

            for (var i = 0; i <= numerOfWorkers; i++) {
                workers.Add(new Worker());
            }

            return workers;
        }
    }
}
