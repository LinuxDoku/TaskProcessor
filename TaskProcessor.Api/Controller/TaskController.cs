using System.Web.Http;

namespace TaskProcessor.Api.Controller
{
	public class TaskController : ApiController
	{
		public string Get(int id) {
			return "Hello World";
		}
	}
}

