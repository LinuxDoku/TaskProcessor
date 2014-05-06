using Microsoft.Owin.Hosting;
using System;

namespace TaskProcessor.Api
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string baseAddress = "http://localhost:9000/";

			WebApp.Start<Startup>(url: baseAddress);

			Console.ReadLine();
		}
	}
}