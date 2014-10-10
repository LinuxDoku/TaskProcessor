using System;
using Microsoft.AspNet.SignalR;
using TaskProcessor.DI;

namespace TaskProcessor.Signalr {
    internal class SignalrDependencyResolver : DefaultDependencyResolver {
        public override object GetService(Type serviceType) {
            return Container.GetExport(serviceType) ?? base.GetService(serviceType);
        }
    }
}
