using System;
using TaskProcessor.Communication.Contract;

namespace TaskProcessor.Communication {
    public class Client<T> : IClient<T> where T : IService {
        private readonly T _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public Client(T service) {
            _service = service;
        }

        public TResult Invoke<TResult>(Func<T, TResult> func) {
            return func(_service);
        }
    }
}
