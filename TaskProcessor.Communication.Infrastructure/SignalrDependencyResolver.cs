using System;
using Microsoft.AspNet.SignalR;
using TaskProcessor.DI;

namespace TaskProcessor.Communication.Infrastructure {
    internal class SignalrDependencyResolver : DefaultDependencyResolver {
        public override object GetService(Type serviceType) {
            return Container.GetExport(serviceType) ?? base.GetService(serviceType);
        }
    }
}
