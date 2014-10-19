using System;
using System.Collections.Generic;

namespace TaskProcessor.Contract.Task {
    public interface ITaskRegistry {
        /// <summary>
        /// List of all registered tasks.
        /// </summary>
        IDictionary<string, Type> Tasks { get; }

        void Register(string typeName);
        void Register(ITask task);
        void Register(Type type);
        void Delete(string taskName);
        void Delete(ITask task);
        void Delete(Type taskType);
    }
}
