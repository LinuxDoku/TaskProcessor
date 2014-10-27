using Microsoft.AspNet.SignalR;
using Owin;

namespace TaskProcessor.Communication.Infrastructure {
    public class OwinStartup {
        public void Configuration(IAppBuilder app) {
            // signalr
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver = new SignalrDependencyResolver();
            app.MapSignalR(hubConfiguration);
        }
    }
}