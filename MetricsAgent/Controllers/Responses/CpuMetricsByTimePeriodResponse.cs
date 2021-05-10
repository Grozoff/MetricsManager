using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public List<CpuMetricDto> Response { get; set; }
    }
    public class CpuMetricDto
    {
        public int Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
