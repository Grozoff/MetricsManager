using System;

namespace MetricsAgent.Controllers.Requests
{
    public class DotNetMetricRequest
    {
        public DotNetMetricRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }
        public DotNetMetricRequest()
        {

        }

        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
