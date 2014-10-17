using System;
using System.Collections.Generic;

namespace TaskProcessor.Contract.Task {
    public interface ITaskManager {
        void Register(string typeName);
        void Register(ITask task);
        ITaskExecution Create(string taskName, object parameter=null);
        ITaskExecution Create(string taskName, DateTime dateTime, object parameter=null);
    }
}