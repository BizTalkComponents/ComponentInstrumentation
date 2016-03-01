using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.BizTalk.Component.Interop;
using System.Reflection;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public class ComponentInstrumentationHelper
    {
        private readonly IComponentTracker _componentInstrumentation;

        private Stopwatch stopWatch = new Stopwatch();
        private DateTime startTime;
        private string version;

        public ComponentInstrumentationHelper(IComponentTracker componentInstrumentation)
        {
            if (componentInstrumentation == null)
            {
                throw new ArgumentNullException("componentInstrumentation");
            }

            _componentInstrumentation = componentInstrumentation;
            startTime = DateTime.UtcNow;
            stopWatch.Start();
            version = getVersion();
        }

        public void TrackComponentException(Exception ex, string componentName)
        {
            stopWatch.Stop();
            _componentInstrumentation.TrackComponentException(ex, startTime, stopWatch.Elapsed, componentName, version);
        }


        public void TrackExecution(string componentName)
        {
            stopWatch.Stop();

            _componentInstrumentation.TrackExecution(startTime, componentName, stopWatch.Elapsed, version);
            stopWatch.Reset();
        }

        private string getVersion()
        {
            var assembly = Assembly
               .GetCallingAssembly();

            return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
        }

    }
}
