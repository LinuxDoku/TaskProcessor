using System;
using System.Linq;
using NUnit.Framework;
using TaskProcessor.Configuration;

namespace TaskProcessor.Tests {
    /// <summary>
    /// Stress test scenarios for config parsers.
    /// </summary>
    [TestFixture]
    class ConfigurationTest {

        [Test]
        public void JsonConfigurationEmptyTest() {
            var configurationString = "{}";
            var config = new JsonConfiguration();
            config.Parse(configurationString);

            Assert.NotNull(config);
            Assert.AreEqual(config.Workers, 0);
            Assert.AreEqual(config.Tasks.Count(), 0);
        }

        [Test]
        public void JsonConfigurationMissingTasksTest() {
            var configurationString = "{\"workers\": 2}";
            var config = new JsonConfiguration();
            config.Parse(configurationString);

            Assert.NotNull(config);
            Assert.AreEqual(config.Workers, 2);
            Assert.AreEqual(config.Tasks.Count(), 0);
        }

        [Test]
        public void JsonConfigurationInvalidJsonTest()
        {
            var configurationString = "{\"workers: 2}";

            try {
                var config = new JsonConfiguration();
                config.Parse(configurationString);
            } catch (Exception ex) {
                Assert.True(ex.Message.ToLower().Contains("invalid configuration"));
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}
