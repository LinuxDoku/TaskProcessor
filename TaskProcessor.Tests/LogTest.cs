using System;
using NUnit.Framework;
using TaskProcessor.Contract;

namespace TaskProcessor.Tests {
    [TestFixture]
    class LogTest {

        [Test]
        public void LogMessageTest()
        {
            var log = new Log("An error occured!");

            Assert.NotNull(log.Time);
            Assert.NotNull(log.Message);
            Assert.AreEqual(log.Message, "An error occured!");
        }

        [Test]
        public void LogExceptionTest()
        {
            ILog log = null;

            try
            {
                var divideBy = 0;
                var result = 1 / divideBy;
            }
            catch (Exception ex)
            {
                log = new Log(ex);
            }

            Assert.NotNull(log);
            Assert.NotNull(log.Time);
            Assert.NotNull(log.Message);
            Assert.True(log.Message.Contains("LogExceptionTest"));
        }

    }
}
