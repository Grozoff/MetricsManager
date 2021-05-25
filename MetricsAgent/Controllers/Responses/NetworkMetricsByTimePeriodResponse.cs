using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public IEnumerable<NetworkMetricDto> Response { get; set; }
    }
    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
