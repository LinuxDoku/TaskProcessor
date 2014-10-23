using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TaskProcessor.Contract.Task;
using TaskProcessor.Task;

namespace TaskProcessor.Tests
{
    [TestFixture()]
    class TaskManagerTest
    {
        private class TestTask : ITask {
            public string Name { get { return "TestTask"; } }
            public void Execute(ITaskConfiguration configuration) {}
        }

        [Test]
        public void RegisterTaskTest() {
            var registryMock = new Mock<ITaskRegistry>();
            registryMock.Setup(x => x.Register(It.Is<string>(y => y.Contains("TestTask"))));

            var taskManager = new TaskManager(registryMock.Object);
            taskManager.Register(typeof(TestTask).FullName);

            registryMock.VerifyAll();
        }

        [Test]
        public void CreateTaskTest() {
            var registryMock = new Mock<ITaskRegistry>();
            registryMock.SetupGet(x => x.Tasks).Returns(new Dictionary<string, Type>() {{ typeof(TestTask).FullName, typeof(TestTask) }});

            var taskManger = new TaskManager(registryMock.Object);
            var execution = taskManger.Create("TaskProcessor.Tests.TaskManagerTest+TestTask", DateTime.Now, null);

            Assert.NotNull(execution);
            Assert.NotNull(execution.Task);
            Assert.AreSame(execution.Task.GetType(), typeof (TestTask));
        }
    }
}
