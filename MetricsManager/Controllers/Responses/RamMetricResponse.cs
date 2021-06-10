using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class RamGetMetricsFromAgentResponse
    {
        public IEnumerable<RamMetricResponse> Response { get; set; }
    }

    public class RamGetMetricsFromClusterResponse
    {
        public IEnumerable<RamMetricResponse> Response { get; set; }
    }
    public class RamMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
