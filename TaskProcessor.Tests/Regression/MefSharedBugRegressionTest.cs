using System;
using System.Threading;
using NUnit.Framework;
using System.Composition;
using System.Composition.Hosting;

namespace TaskProcessor.Tests.Regression
{
    [TestFixture]
    public class MefSharedBugRegressionTest {
        private static CompositionHost Container;

        private interface ISharedManager {
            int GetCounter();
        }

        [Export(typeof(ISharedManager))]
        [Shared]
        private class SharedManager : ISharedManager
        {
            private int _counter = 0;

            public int GetCounter()
            {
                return ++_counter;
            }
        }

        [Test]
        public void SharedTest()
        {
            var containerConfiguration = new ContainerConfiguration();
            containerConfiguration.WithPart<SharedManager>();

            Container = containerConfiguration.CreateContainer();

            Assert.AreEqual(1, Container.GetExport<ISharedManager>().GetCounter());
            Assert.AreEqual(2, Container.GetExport<ISharedManager>().GetCounter());

            new Thread(() => {
                Assert.AreEqual(3, Container.GetExport<ISharedManager>().GetCounter());
            }).Start();
        }
    }
}
