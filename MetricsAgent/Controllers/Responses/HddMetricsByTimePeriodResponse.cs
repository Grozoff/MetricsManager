using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public List<HddMetricDto> Response { get; set; }
    }
    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
