using Microsoft.AspNet.SignalR;
using Owin;

namespace TaskProcessor.Communication.Infrastructure {
    public class OwinStartup {
        public void Configuration(IAppBuilder app) {
            // signalr
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver = new SignalrDependencyResolver();
#if DEBUG
            hubConfiguration.EnableDetailedErrors = true;
#endif
            app.MapSignalR(hubConfiguration);
        }
    }
}