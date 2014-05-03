using System;
using TaskProcessor.Contracts;
using TaskProcessor.Queue;
using TaskProcessor.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskProcessor
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var queue = new MemoryQueue();
			var runner = new TaskRunner(queue);

			// try to read config
			var configFile = "./config.json";

			if(File.Exists(configFile)) {
				var configFileText = File.ReadAllText(configFile);
				var config = JObject.Parse(configFileText);

				if(config != null) {
					foreach(var task in config.SelectToken("tasks")) {
						var taskName = (string)task.SelectToken("name");
						var taskMessage = (string)task.SelectToken("message");

						var messageTask = new MessageTask();
						messageTask.Message = taskMessage;
						queue.Add(messageTask);
					}
				}
			}
		}
	}
}