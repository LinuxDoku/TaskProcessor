using System;
using System.Collections;

namespace TaskProcessor.Contracts
{
	/// <summary>
	/// Interface for a worker which processes tasks.
	/// </summary>
	public interface IWorker
	{
		/// <summary>
		/// Start the worker.
		/// </summary>
		void Start();

		/// <summary>
		/// Stop the worker - now!
		/// </summary>
		void Stop();

		/// <summary>
		/// Cancel the worker execution. (The worker stops in the next best moment).
		/// </summary>
		void Cancel();

		/// <summary>
		/// Abort a cancel.
		/// </summary>
		void AbortCancel();
	}
}