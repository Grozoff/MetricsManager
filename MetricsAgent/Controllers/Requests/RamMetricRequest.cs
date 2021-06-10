using System;

namespace MetricsAgent.Controllers.Requests
{
    public class RamMetricRequest
    {
        public RamMetricRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }
        public RamMetricRequest()
        {

        }

        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
