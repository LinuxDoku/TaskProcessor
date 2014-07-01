using System;
using TaskProcessor.Queue;
using System.IO;
using TaskProcessor.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Mime;
using Newtonsoft.Json.Linq;
using Microsoft.Owin.Hosting;
using System.Net.Sockets;

namespace TaskProcessor
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var queue = new MemoryQueue();

			// try to read config
			var configFile = "./config.json";

			if(File.Exists(configFile)) {
				var configFileText = File.ReadAllText(configFile);
				var config = JObject.Parse(configFileText);

				if(config != null) {
					// start workeres
					var workers = new Action[(int)config.SelectToken("workers")];

					if(workers.Length < 1) {
						Console.WriteLine("Please add more than one worker to the config!");
						return;
					}

					for(var i = 0; i < workers.Length; i++) {
                        queue.Add(new Worker());
                        workers[i] = () => new Worker();
					}

                    //Parallel.Invoke(workers);

					// add tasks to queue
					foreach(var taskConfig in config.SelectToken("tasks")) {
						ITask task = null;

						var assemblyName = (string)taskConfig.SelectToken("assembly");
						var className = (string)taskConfig.SelectToken("class");
						var argumentList = taskConfig.SelectToken("arguments");

						var typeName = className;
						if(assemblyName != null) {
							typeName = className + "," + assemblyName;
						}

						var type = Type.GetType(typeName);
						foreach(var constructor in type.GetConstructors()) {
							var parameters = constructor.GetParameters();
							if(parameters.Length == argumentList.Count()) {
								// try with this constructor
								try {
									var arguments = new List<Object>();

									for(var i = 0; i < parameters.Length; i++) {
										arguments.Add(argumentList[i].ToObject(parameters[i].ParameterType));
									}
										
									task = (ITask)Activator.CreateInstance(type, arguments.ToArray());
									break;
								} catch {
									continue;
								}
							}
						}

						if(task != null) {
                            var taskExecution = new TaskExecution(task);
                            queue.Add(taskExecution);
						} else {
							Console.WriteLine("No Task or suiteable constructor found! Task Name: '" + task.Name + "'");
						}
					}
				}
			} else {
				Console.WriteLine("No 'config.json' found!");
				return;
			}

            var host = "http://localhost:8080";
            try {
                using(WebApp.Start<Startup>(host)) {
                    Console.WriteLine("Server is running");
                    Console.ReadLine();
                }
            } catch(TargetInvocationException exception) {
                if(exception.InnerException.GetType() == typeof(SocketException)) {
                    Console.WriteLine("Port is already in use!");
                } else {
                    Console.WriteLine("Someting went wrong! " + exception.InnerException.Message);
                }
            }
		}
	}
}