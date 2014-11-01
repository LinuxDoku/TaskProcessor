using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using TaskProcessor.Communication.Contract;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IHubDescriptorProvider))]
    public class HubDescriptorProvider : IHubDescriptorProvider {
        /// <summary>
        /// Retrieve all avaiable hubs.
        /// </summary>
        /// <returns>
        /// Collection of hub descriptors.
        /// </returns>
        public IList<HubDescriptor> GetHubs() {
            var hubDescriptors = new List<HubDescriptor>();
            var services = DI.Container.GetExports<IService>();

            foreach (var service in services) {
                var type = service.GetType();

                hubDescriptors.Add(new HubDescriptor() {
                    HubType = type,
                    Name = type.Name,
                    NameSpecified = true
                });
            }

            return hubDescriptors;
        }

        /// <summary>
        /// Tries to retrieve hub with a given name.
        /// </summary>
        /// <param name="hubName">Name of the hub.</param><param name="descriptor">Retrieved descriptor object.</param>
        /// <returns>
        /// True, if hub has been found
        /// </returns>
        public bool TryGetHub(string hubName, out HubDescriptor descriptor) {
            descriptor = GetHubs().FirstOrDefault(x => x.Name == hubName);

            if (descriptor != null) {
                return true;
            }

            return false;
        }
    }
}
