using System;
using TaskProcessor.Contracts;
using System.Collections.Generic;

namespace TaskProcessor
{
    public class TaskExecution : ITaskExecution
    {
        protected ITask _task;
        protected TaskStatus _status;

        public TaskExecution(ITask task)
        {
            _task = task;

            Log = new Dictionary<DateTime, string>();
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
                Log.Add(DateTime.Now, "Status Changed: '" + value + "'");
            }
        }

        public string Output { get; set; }

        public List<Exception> Exceptions { get; set; }

        public Dictionary<DateTime, string> Log { get; set; }

        #endregion
    }
}

