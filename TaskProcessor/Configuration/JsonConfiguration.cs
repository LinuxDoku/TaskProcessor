using System;
using System.Collections;
using TaskProcessor.Contracts;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TaskProcessor.Configuration
{
    public class JsonConfiguration : IConfiguration
    {
        private int _workers;
        private IEnumerable<ITask> _tasks;

        public JsonConfiguration(string jsonString)
        {
            var source = JObject.Parse(jsonString);
            if (source != null) { 
                _workers = (int)source.SelectToken("workers");
                _tasks = Enumerable.Empty<ITask>();
            }
        }

        #region IConfiguration implementation

        public int Workers
        {
            get { return _workers; }
        }

        public System.Collections.Generic.IEnumerable<TaskProcessor.Contracts.ITask> Tasks
        {
            get { return _tasks; }
        }

        #endregion
    }
}

