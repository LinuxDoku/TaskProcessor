using System;
using System.Collections.Generic;
using TaskProcessor.Contracts;

namespace TaskProcessor.Tasks {
    public interface ITaskManager {
        bool RegisterTask(string taskName, string typeName);
        bool RegisterTask(string taskName, Type type);
        ITask Create(string taskName, object parameter = null);
        IEnumerable<string> GetAll();
    }
}