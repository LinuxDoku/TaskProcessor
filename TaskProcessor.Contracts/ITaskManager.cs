using System;
using System.Collections.Generic;

namespace TaskProcessor.Contracts {
    public interface ITaskManager {
        bool RegisterTask(string taskName, string typeName);
        bool RegisterTask(string taskName, Type type);
        ITask Create(string taskName, object parameter = null);
        IEnumerable<string> GetAll();
    }
}