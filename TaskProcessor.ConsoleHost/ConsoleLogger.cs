using System;
using TaskProcessor.Contract;

namespace TaskProcessor.ConsoleHost {
    class ConsoleLogger : ILogger {
        public void Log(string message) {
            Console.WriteLine(message);
        }
    }
}
