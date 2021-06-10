using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class HddGetMetricsFromAgentResponse
    {
        public IEnumerable<HddMetricResponse> Response { get; set; }
    }

    public class HddGetMetricsFromClusterResponse
    {
        public IEnumerable<HddMetricResponse> Response { get; set; }
    }
    public class HddMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
