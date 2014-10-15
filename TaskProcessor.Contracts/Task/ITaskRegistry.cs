﻿using System.Collections.Generic;
using TaskProcessor.Contracts;

namespace TaskProcessor.Contract.Task {
    public interface ITaskRegistry {
        /// <summary>
        /// List of all registered tasks.
        /// </summary>
        IEnumerable<ITask> Tasks { get; }

        void Register(string typeName);
        void Register(ITask task);
        void Delete(string taskName);
        void Delete(ITask task);
    }
}