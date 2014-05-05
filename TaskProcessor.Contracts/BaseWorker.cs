namespace TaskProcessor.Contracts
{
	public abstract class BaseWorker : IWorker
	{
		ITaskQueue Queue { get; }

		public BaseWorker(ITaskQueue queue)
		{
			Queue = queue;
		}

		abstract public void Start();
		abstract public void Stop();
		abstract public void Cancel();
		abstract public void AbortCancel();
	}
}