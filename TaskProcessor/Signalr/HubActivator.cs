using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace TaskProcessor.Signalr {
    [Export(typeof(IHubActivator))]
    public class HubActivator : IHubActivator {
        public IHub Create(HubDescriptor descriptor) {
            return DI.GetExport(descriptor.HubType) as IHub;
        }
    }
}
