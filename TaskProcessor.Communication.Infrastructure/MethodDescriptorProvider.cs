using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Services;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Json;
using TaskProcessor.Communication.Contract;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Communication.Infrastructure {
    [Export(typeof(IMethodDescriptorProvider))]
    public class MethodDescriptorProvider : IMethodDescriptorProvider {
        /// <summary>
        /// Retrieve all methods on a given hub.
        /// </summary>
        /// <param name="hub">Hub descriptor object.</param>
        /// <returns>
        /// Available methods.
        /// </returns>
        public IEnumerable<MethodDescriptor> GetMethods(HubDescriptor hub) {
            var methodDescriptors = new List<MethodDescriptor>();
            var methods = hub.HubType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods) {
                methodDescriptors.Add(new MethodDescriptor() {
                    ReturnType = method.ReturnType,
                    Name = method.Name,
                    NameSpecified = true,
                    Attributes = method.GetCustomAttributes(typeof(Attribute), true).Cast<Attribute>(),
                    Hub = hub,
                    Invoker = CreateInvoker(method)
                });
            }

            return methodDescriptors;
        }

        private Func<IHub, object[], object> CreateInvoker(MethodInfo method) {
            return ((hub, args) => {
                var hubContainer = hub as HubContainer<IService>;

                if (hubContainer != null) {
                    return hubContainer.Execute(x => method.Invoke(x, args));
                }

                return null;
            });
        }

        /// <summary>
        /// Tries to retrieve a method.
        /// </summary>
        /// <param name="hub">Hub descriptor object</param><param name="method">Name of the method.</param><param name="descriptor">Descriptor of the method, if found. Null otherwise.</param><param name="parameters">Method parameters to match.</param>
        /// <returns>
        /// True, if a method has been found.
        /// </returns>
        public bool TryGetMethod(HubDescriptor hub, string method, out MethodDescriptor descriptor, IList<IJsonValue> parameters) {
            descriptor = GetMethods(hub).FirstOrDefault(x => x.Name == method);

            if (descriptor != null) {
                return true;
            }

            return false;
        }
    }
}
