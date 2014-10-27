using System;
using Microsoft.Owin.Hosting;
using Owin;
using TaskProcessor.Communication.Contract;
using TaskProcessor.Communication.Contract.Infrastructure;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IServerImplementation))]
    public class Server : IServerImplementation {
        public void Start(string hostUri) {
            Start(new Uri(hostUri));
        }

        public void Start(Uri uri) {
            using (WebApp.Start<OwinStartup>(uri.AbsoluteUri)) {
                // TODO
            }
        }

        public void Start(IAppBuilder owinAppBuilder) {
            var owinStartup = new OwinStartup();
            owinStartup.Configuration(owinAppBuilder);
        }
    }
}
