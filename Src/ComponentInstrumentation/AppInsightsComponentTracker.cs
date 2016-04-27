using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace BizTalkComponents.Utilities.ComponentInstrumentation
{
    public class AppInsightsComponentTracker : IComponentTracker
    {
        private readonly TelemetryClient tc = new TelemetryClient();

        public AppInsightsComponentTracker(string trackingApiKey)
        {
            if(string.IsNullOrEmpty(trackingApiKey))
            {
                throw new ArgumentNullException("trackingApiKey");
            }

            TelemetryConfiguration.Active.InstrumentationKey = trackingApiKey;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
        }

        public void TrackComponentError(DateTime endDateTime, TimeSpan duration, Exception ex, string componentName, string componentVersion)
        {
            endDateTime = DateTime.SpecifyKind(endDateTime, DateTimeKind.Utc);
            tc.Context.Component.Version = componentVersion;

            tc.TrackException(ex);
        }

        public void TrackComponentSuccess(DateTime endDateTime, TimeSpan duration, string componentName, string componentVersion)
        {
            endDateTime = DateTime.SpecifyKind(endDateTime, DateTimeKind.Utc);
            tc.Context.Component.Version = componentVersion;
            tc.TrackRequest(componentName, endDateTime, duration, "200", true);
            var pv = new PageViewTelemetry();
            pv.Duration = duration;
            pv.Name = componentName;
            pv.Timestamp = endDateTime;
            tc.TrackPageView(pv);
        }
    }
}
