using System;
using Microsoft.AspNet.SignalR;

namespace TaskProcessor.Signalr {
    internal class SignalrDependencyResolver : DefaultDependencyResolver {
        public override object GetService(Type serviceType) {
            return DI.GetExport(serviceType) ?? base.GetService(serviceType);
        }
    }
}
