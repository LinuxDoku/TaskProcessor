using System;
using TaskProcessor.Contracts;
using System.Collections.Generic;

namespace TaskProcessor
{
    public class TaskExecution : ITaskExecution
    {
        protected ITask _task;
        protected TaskStatus _status;
        protected readonly IList<ILog> _logs;

        public TaskExecution(ITask task)
        {
            _task = task;
            _logs = new List<ILog>();

            Status = TaskStatus.INITIAL;
        }

        #region ITaskExecution implementation

        public ITask Task 
        { 
            get { return _task; }
        }

        public TaskStatus Status
        {
            get { return _status; }  
            set { 
                _status = value; 
                Log("Status Changed: '" + value + "'");
            }
        }

        public string Output { get; set; }

        public IEnumerable<ILog> Logs { get { return _logs; } }

        public void Log(ILog log)
        {
            _logs.Add(log);
        }

        public void Log(string message)
        {
            Log(new Log(message));
        }

        public void Log(Exception exception)
        {
            Log(new Log(exception));
        }

        #endregion
    }
}

