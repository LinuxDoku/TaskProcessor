using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TaskProcessor.Contract.Task;

namespace TaskProcessor.Contracts.Configuration
{
    public interface IConfiguration {
        /// <summary>
        /// Parse the given configuration.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        void Parse(string source);

        /// <summary>
        /// Number of worker instances on this machine.
        /// </summary>
        /// <value>The workers.</value>
        int Workers { get; } 

        /// <summary>
        /// Tasks to execute on this machine.
        /// </summary>
        /// <value>The tasks.</value>
        IEnumerable<ITask> Tasks { get; }

        /// <summary>
        /// Should the integrated communcation service use https?
        /// </summary>
        bool UseHttps { get; }

        /// <summary>
        /// Hostname for the integrated communication service.
        /// </summary>
        string Hostname { get; }

        /// <summary>
        /// Port for the integrated communication service.
        /// </summary>
        short Port { get; }
    }
}