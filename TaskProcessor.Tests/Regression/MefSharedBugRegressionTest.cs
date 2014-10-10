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

        private class DI
        {
            private static DI instance = new DI();
            private CompositionHost _container;

            private DI()
            {
                var containerConfiguration = new ContainerConfiguration();
                containerConfiguration.WithPart<SharedManager>();

                _container = containerConfiguration.CreateContainer();
            }

            public static T GetExport<T>() {
                return DI.instance._container.GetExport<T>();
            }
        }

        private class ThreadWorker
        {
            public static void Work() {
                Assert.AreEqual(3, DI.GetExport<ISharedManager>().GetCounter());
            }
        }

        [Test]
        public void SharedTest()
        {
            Assert.AreEqual(1, DI.GetExport<ISharedManager>().GetCounter());
            Assert.AreEqual(2, DI.GetExport<ISharedManager>().GetCounter());

            new Thread(ThreadWorker.Work).Start();

            Assert.AreEqual(4, DI.GetExport<ISharedManager>().GetCounter());
        }
    }
}
