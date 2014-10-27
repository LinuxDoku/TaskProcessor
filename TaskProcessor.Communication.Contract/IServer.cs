using System;
using Owin;

namespace TaskProcessor.Communication.Contract {
    public interface IServer {
        void Start(IAppBuilder owinAppBuilder);
    }
}
