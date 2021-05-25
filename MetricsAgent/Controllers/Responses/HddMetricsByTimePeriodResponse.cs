using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public IEnumerable<HddMetricDto> Response { get; set; }
    }
    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
