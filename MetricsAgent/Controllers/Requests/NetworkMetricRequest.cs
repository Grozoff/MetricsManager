using System;

namespace MetricsAgent.Controllers.Requests
{
    public class NetworkMetricRequest
    {
        public NetworkMetricRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }
        public NetworkMetricRequest()
        {

        }

        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
