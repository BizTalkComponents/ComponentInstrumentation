using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public interface IComponentTracker
    {
        void TrackComponentStartEvent(string componentName, string componentStage);
        void TrackComponentException(Exception ex);
        void TrackDuration(TimeSpan timeSpan);
        void Finish();
    }
}
