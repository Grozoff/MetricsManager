using System;

namespace MetricsAgent.Controllers.Requests
{
    public class HddMetricRequest
    {
        public HddMetricRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }
        public HddMetricRequest()
        {

        }

        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
