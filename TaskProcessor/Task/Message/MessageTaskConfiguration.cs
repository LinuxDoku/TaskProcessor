using TaskProcessor.Contract.Task;

namespace TaskProcessor.Task.Message {
    public class MessageTaskConfiguration : ITaskConfiguration {
        public string Message { get; set; }
    }
}
