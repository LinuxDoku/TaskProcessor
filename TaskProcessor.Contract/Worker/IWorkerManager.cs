using System.Collections.Generic;

namespace TaskProcessor.Contract.Worker
{
    public interface IWorkerManager
    {
        IEnumerable<IWorker> Spawn(int numerOfWorkers);
    }
}
