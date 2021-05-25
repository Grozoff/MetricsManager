using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public IEnumerable<RamMetricDto> Response { get; set; }
    }
    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
