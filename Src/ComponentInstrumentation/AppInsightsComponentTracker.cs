using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public void TrackComponentException(Exception ex, DateTime startDateTime, TimeSpan duration, string componentName, string componentVersion)
        {
            startDateTime = DateTime.SpecifyKind(startDateTime, DateTimeKind.Utc);
            tc.TrackRequest(componentName, startDateTime, duration, "500", false);
            tc.Context.Component.Version = componentVersion;
            tc.TrackException(ex);
        }

        public void TrackExecution(DateTime startDateTime, string componentName, TimeSpan duration, string componentVersion)
        {
            startDateTime = DateTime.SpecifyKind(startDateTime, DateTimeKind.Utc);
            tc.Context.Component.Version = componentVersion;
            tc.TrackRequest(componentName, startDateTime, duration, "200", true);
        }
    }
}
