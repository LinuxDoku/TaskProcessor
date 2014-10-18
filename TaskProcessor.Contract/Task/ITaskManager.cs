using System;
using System.Collections.Generic;

namespace TaskProcessor.Contract.Task {
    public interface ITaskManager {
        void Register(string typeName);
        void Register(ITask<ITaskConfiguration> task);
        ITaskExecution Create(string taskName, ITaskConfiguration configuration=null);
        ITaskExecution Create(string taskName, DateTime dateTime, ITaskConfiguration configuration=null);
    }
}