using System.Collections.Generic;

namespace TaskProcessor.Contracts.Hubs {
    public interface ITasksHub {
        IEnumerable<string> GetTasks();
    }
}
