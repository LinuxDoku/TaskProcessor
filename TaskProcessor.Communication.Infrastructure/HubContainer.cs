using System;
using Microsoft.AspNet.SignalR;
using TaskProcessor.Communication.Contract;

namespace TaskProcessor.Communication.Infrastructure {
    public class HubContainer<TService> : Hub where TService : IService {
        private readonly TService _service;

        public HubContainer(TService service) {
            _service = service;
        } 

        public object Execute(Func<TService, object> func) {
            return func(_service);
        }

        public T Execute<T>(Func<TService, T> func) {
            return func(_service);
        }
    }
}
