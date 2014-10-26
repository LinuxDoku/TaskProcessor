using System;
using NUnit.Framework;
using TaskProcessor.Communication;
using TaskProcessor.Communication.Contract;

namespace TaskProcessor.Tests {
    [TestFixture]
    public class CommunicationClient {
        private class TestService : IService {
            public string HelloWorld() {
                return "Hello World";
            }
        }

        private class TestClient : IClient<TestService> {
            private readonly TestService _service;

            public TestClient(TestService service) {
                _service = service;
            }

            public TResult Invoke<TResult>(Func<TestService, TResult> func) {
                return func(_service);
            }
        }

        [Test]
        public void CommunicationClientInvokeTest() {
            var service = new TestService();
            var client = new TestClient(service);

            Assert.AreEqual("Hello World", client.Invoke(x => x.HelloWorld()));
        }

        [Test]
        public void CommunicationClientBaseInvokeTest() {
            var service = new TestService();
            var client = new Client<TestService>(service);

            Assert.AreEqual("Hello World", client.Invoke(x => x.HelloWorld()));
        }
    }
}
