using System;
using System.Linq;
using NUnit.Framework;
using TaskProcessor.Configuration;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contracts;

namespace TaskProcessor.Tests {
    /// <summary>
    /// Stress test scenarios for config parsers.
    /// </summary>
    [TestFixture]
    class ConfigurationTest {

        [Test]
        public void JsonConfigurationEmptyTest() {
            var taskManagerMock = new Moq.Mock<ITaskManager>();

            var configurationString = "{}";
            var config = new JsonConfiguration(taskManagerMock.Object);
            config.Parse(configurationString);

            Assert.NotNull(config);
            Assert.AreEqual(config.Workers, 0);
            Assert.AreEqual(config.Tasks.Count(), 0);
        }

        [Test]
        public void JsonConfigurationMissingTasksTest() {
            var taskManagerMock = new Moq.Mock<ITaskManager>();

            var configurationString = "{\"workers\": 2}";
            var config = new JsonConfiguration(taskManagerMock.Object);
            config.Parse(configurationString);

            Assert.NotNull(config);
            Assert.AreEqual(config.Workers, 2);
            Assert.AreEqual(config.Tasks.Count(), 0);
        }

        [Test]
        public void JsonConfigurationInvalidJsonTest()
        {
            var taskManagerMock = new Moq.Mock<ITaskManager>();

            var configurationString = "{\"workers: 2}";

            try {
                var config = new JsonConfiguration(taskManagerMock.Object);
                config.Parse(configurationString);
            } catch (Exception ex) {
                Assert.True(ex.Message.ToLower().Contains("invalid configuration"));
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}
