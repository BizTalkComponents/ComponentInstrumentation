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

        public AppInsightsComponentTracker()
        {
            TelemetryConfiguration.Active.InstrumentationKey = "3e9ea04c-ebdf-4070-bb39-f3f57967aa16";
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Properties["client"] = getClientId();
        }

        public void TrackComponentStartEvent(string componentName, string componentStage)
        {
            var et = new EventTelemetry("execute");
            et.Properties["stageid"] = componentStage;
            et.Properties["componentname"] = componentName;
            tc.TrackEvent(et);
            tc.TrackPageView(componentName);
        }

        public void TrackDuration(TimeSpan timeSpan)
        {
            tc.TrackMetric("Execution duration", timeSpan.TotalMilliseconds);
        }

        public void TrackComponentException(Exception ex)
        {
            tc.TrackException(ex);
        }

        public void Finish()
        {
            tc.Flush();
        }

        private string getClientId()
        {
            var md5 = MD5.Create();

            var bytes = Encoding.ASCII.GetBytes(Environment.MachineName);
            var hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
