using System;
using TaskProcessor.Contracts;

namespace TaskProcessor
{
    /// <summary>
    /// Logs entry.
    /// </summary>
    public class Log : ILog
    {
        protected readonly DateTime _time;
        protected readonly string _message;

        public Log(string message)
        {
            _time = DateTime.Now;
            _message = message;
        }

        public Log(Exception exception)
        {
            _time = DateTime.Now;
            _message = exception.ToString();
        }

        /// <summary>
        /// Timestamp when the log entry was created.
        /// </summary>
        public DateTime Time { get { return _time; } }

        /// <summary>
        /// Logs message.
        /// </summary>
        public string Message { get { return _message; } }
    }
}
