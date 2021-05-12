using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public List<RamMetricDto> Response { get; set; }
    }
    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
