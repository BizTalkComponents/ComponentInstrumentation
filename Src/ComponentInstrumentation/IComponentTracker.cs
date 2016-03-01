using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public interface IComponentTracker
    {
        void TrackExecution(DateTime startDateTime, string componentName, TimeSpan duration, string componentVersion);
        void TrackComponentException(Exception ex, DateTime startDateTime, TimeSpan duration, string componentName, string componentVersion);
    }
}
