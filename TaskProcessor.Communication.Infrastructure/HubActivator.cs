using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IHubActivator))]
    public class HubActivator : IHubActivator {
        public IHub Create(HubDescriptor descriptor) {
            return Container.GetExport(descriptor.HubType) as IHub;
        }
    }
}
