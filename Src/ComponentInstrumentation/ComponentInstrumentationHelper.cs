using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public class ComponentInstrumentationHelper
    {
        private readonly IComponentTracker _componentInstrumentation;

        private Stopwatch stopWatch = new Stopwatch();
        private DateTime startTime;
        private string version;
        private string _componentName;

        public ComponentInstrumentationHelper(IComponentTracker componentInstrumentation, string componentName)
        {
            if (componentInstrumentation == null)
            {
                throw new ArgumentNullException("componentInstrumentation");
            }

            if(string.IsNullOrEmpty(componentName))
            {
                throw new ArgumentNullException("componentName");
            }

            _componentInstrumentation = componentInstrumentation;
            startTime = DateTime.UtcNow;
            stopWatch.Start();
            version = getVersion();
            _componentName = componentName;
        }

        public void TrackComponentError(Exception ex)
        {
            stopWatch.Stop();
            _componentInstrumentation.TrackComponentError(DateTime.UtcNow,stopWatch.Elapsed, ex, _componentName, version);
        }
        
        public void TrackComponentSuccess()
        {
            stopWatch.Stop();
            _componentInstrumentation.TrackComponentSuccess(DateTime.UtcNow, stopWatch.Elapsed, _componentName, version);
        }
        private string getVersion()
        {
            var assembly = Assembly
               .GetCallingAssembly();

            return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
        }

    }
}
