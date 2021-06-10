using System;

namespace MetricsAgent.Controllers.Requests
{
    public class CpuMetricRequest
    {
        public CpuMetricRequest(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public CpuMetricRequest()
        {

        }

        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
