using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.Communication.Contract;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IHubActivator))]
    public class HubActivator : IHubActivator {
        public IHub Create(HubDescriptor descriptor) {
            var service = (IService)DI.Container.GetExport(descriptor.HubType);

            if (service != null) {
                return new HubContainer<IService>(service);
            }

            return null;
        }
    }
}
