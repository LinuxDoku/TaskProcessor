using System;
using TaskProcessor.Contracts;

namespace TaskProcessor.ConsoleHost {
    class ConsoleLogger : ILogger {
        public void Log(string message) {
            Console.WriteLine(message);
        }
    }
}
