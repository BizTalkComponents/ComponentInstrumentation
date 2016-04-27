using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizTalkComponents.Utilities.ComponentInstrumentation;
using Moq;

namespace BizTalkComponents.Utilities.Tests.UnitTests
{
    [TestClass]
    public class ComponentInstrumentationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullTracker()
        {
            var helper = new ComponentInstrumentationHelper(null, "componentName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestEmptyName()
        {
            var mock = new Mock<IComponentTracker>();
            var helper = new ComponentInstrumentationHelper(mock.Object, string.Empty);
        }

        [TestMethod]
        public void TestError()
        {
            var mock = new Mock<IComponentTracker>();
            mock.Setup(componentTracker =>
                componentTracker.TrackComponentError(It.IsAny<DateTime>(), It.IsAny<TimeSpan>(),
                It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var helper = new ComponentInstrumentationHelper(mock.Object, "componentName");
            helper.TrackComponentError(new ArgumentException());
        }

        [TestMethod]
        public void TestSuccess()
        {
            var mock = new Mock<IComponentTracker>();
            mock.Setup(componentTracker =>
                componentTracker.TrackComponentSuccess(It.IsAny<DateTime>(), It.IsAny<TimeSpan>(),
                It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var helper = new ComponentInstrumentationHelper(mock.Object, "componentName");
            helper.TrackComponentSuccess();
        }
    }
}
