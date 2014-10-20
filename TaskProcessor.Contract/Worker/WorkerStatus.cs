namespace TaskProcessor.Contract.Worker
{
    public enum WorkerStatus
    {
        Waiting,
        Working,
        Canceled,
        Paused,
        Stopped
    }
}