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
