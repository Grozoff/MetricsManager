using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public List<CpuMetricDto> Response { get; set; }
    }
    public class CpuMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
