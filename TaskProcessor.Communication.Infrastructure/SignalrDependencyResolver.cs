using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IDependencyResolver))]
    [Shared]
    internal class SignalrDependencyResolver : DefaultDependencyResolver {
        public override object GetService(Type serviceType) {
            return Container.GetExport(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType) {
            return Container.GetExports(serviceType) ??  base.GetServices(serviceType);
        }
    }
}