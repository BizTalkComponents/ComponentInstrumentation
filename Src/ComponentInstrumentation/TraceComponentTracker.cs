using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public class TraceComponentTracker : IComponentTracker
    {
        public void TrackComponentError(DateTime endDateTime, TimeSpan duration, Exception ex, string componentName, string componentVersion)
        {
            Trace.WriteLine(string.Format("{0} version {1} ended execution at {2} with error {3}", componentName, componentVersion, endDateTime, ex.ToString()));
        }
        public void TrackComponentSuccess(DateTime endDateTime, TimeSpan duration, string componentName, string componentVersion)
        {
            Trace.WriteLine(string.Format("{0} version {1} executed successfully at {2} with a duration of {3} ms", componentName, componentVersion, endDateTime, duration.Milliseconds));
        }
    }
}
