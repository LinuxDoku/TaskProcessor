using NUnit.Framework;
using System.Composition;
using System.Composition.Hosting;

namespace TaskProcessor.Tests.Regression
{
    [TestFixture]
    public class MefSharedBugRegressionTest
    {
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

            var container = containerConfiguration.CreateContainer();

            Assert.AreSame(1, container.GetExport<SharedManager>().GetCounter());
        }
    }
}
