namespace TaskProcessor.Contract.Worker
{
    public enum WorkerStatus
    {
        WAITING,
        WORKING,
        CANCELED,
        PAUSED,
        STOPPED
    }
}