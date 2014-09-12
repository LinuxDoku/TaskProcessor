using System;
using TaskProcessor.Contracts;
using System.Collections.Generic;

namespace TaskProcessor
{
    public class TaskExecution : ITaskExecution
    {
        protected ITask _task;
        protected TaskStatus _status;
        protected IList<ILog> _log;

        public TaskExecution(ITask task)
        {
            _task = task;

            _log = new List<ILog>();
            Exceptions = new List<Exception>();

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
                Log.Add(new Log("Status Changed: '" + value + "'"));
            }
        }

        public string Output { get; set; }

        public IList<Exception> Exceptions { get; set; }

        public IList<ILog> Log { get { return _log; } }

        #endregion
    }
}

