using Owin;

namespace TaskProcessor.Communication.Contract.Infrastructure {
    public interface IServerImplementation {
        void Start(IAppBuilder owinAppBuilder);
    }
}
