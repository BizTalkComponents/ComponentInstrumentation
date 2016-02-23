using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.BizTalk.Component.Interop;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public class ComponentInstrumentationHelper
    {
        private readonly IComponentTracker _componentInstrumentation;

        private Stopwatch stopWatch = new Stopwatch();

        public ComponentInstrumentationHelper(IComponentTracker componentInstrumentation)
        {
            if(componentInstrumentation == null)
            {
                throw new ArgumentNullException("componentInstrumentation");
            }

            _componentInstrumentation = componentInstrumentation;
        }

        public void TrackComponentException(Exception ex)
        {
            _componentInstrumentation.TrackComponentException(ex);
        }

        public void TrackComponentStart(IPipelineContext ctx)
        {
            stopWatch.Start();
            _componentInstrumentation.TrackComponentStartEvent(ctx.PipelineName, ctx.StageID.ToString());
        }

        public void TrackComponentEnd()
        {
            stopWatch.Stop();

            _componentInstrumentation.TrackDuration(stopWatch.Elapsed);

            stopWatch.Reset();
            _componentInstrumentation.Finish();

        }
        
    }
}
