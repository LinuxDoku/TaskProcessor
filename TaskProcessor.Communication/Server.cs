using Owin;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Communication.Contract.Infrastructure;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication {
    [Export(typeof(IServer))]
    public class Server : IServer {
        private readonly IServerImplementation _server;

        [Import]
        public Server(IServerImplementation server) {
            _server = server;
        }

        public void Start(IAppBuilder owinAppBuilder) {
            _server.Start(owinAppBuilder);
        }
    }
}
