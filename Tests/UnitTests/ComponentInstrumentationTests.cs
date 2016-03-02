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
        public void TestTrackException()
        {
            var mock = new Mock<IComponentTracker>();
            mock.Setup(componentTracker => componentTracker.TrackComponentException(new ArgumentException("exception"), DateTime.Now, new TimeSpan(1000),"componentName", "componentVersion"));
           
        }
    }
}
