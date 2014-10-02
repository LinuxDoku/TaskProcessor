using Owin;

namespace TaskProcessor {
    public class OwinStartup {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
        }
    }
}