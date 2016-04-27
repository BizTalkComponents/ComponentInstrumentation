using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public interface IComponentTracker
    {
        void TrackComponentSuccess(DateTime endDateTime, TimeSpan duration, string componentName, string componentVersion);
        void TrackComponentError(DateTime endDateTime, TimeSpan duration, Exception ex, string componentName, string componentVersion);
    }
}
