using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor.Contracts;

namespace TaskProcessor
{
    /// <summary>
    /// Log entry.
    /// </summary>
    public class Log : ILog
    {
        protected DateTime _time;
        protected string _message;

        public Log(string message)
        {
            _time = DateTime.Now;
            _message = message;
        }

        /// <summary>
        /// Timestamp when the log entry was created.
        /// </summary>
        public DateTime Time { get { return _time; } }

        /// <summary>
        /// Log message.
        /// </summary>
        public string Message { get { return _message; } }
    }
}
