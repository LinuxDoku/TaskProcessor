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

        [Export]
        [Shared]
        private class SharedManager
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

            Assert.AreEqual(1, Container.GetExport<SharedManager>().GetCounter());
            Assert.AreEqual(2, Container.GetExport<SharedManager>().GetCounter());

            new Thread(() => {
                Assert.AreEqual(3, Container.GetExport<SharedManager>().GetCounter());
            }).Start();
        }
    }
}
