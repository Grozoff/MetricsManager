using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public List<NetworkMetricDto> Response { get; set; }
    }
    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
