using Microsoft.AspNet.SignalR;
using Owin;
using TaskProcessor.Signalr;

namespace TaskProcessor {
    public class OwinStartup {
        public void Configuration(IAppBuilder app) {
            // signalr
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver = new SignalrDependencyResolver();
            app.MapSignalR(hubConfiguration);
        }
    }
}