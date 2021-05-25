using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class DotNetGetMetricsFromAgentResponse
    {
        public IEnumerable<DotNetMetricResponse> Response { get; set; }
    }

    public class DotNetGetMetricsFromClusterResponse
    {
        public IEnumerable<DotNetMetricResponse> Response { get; set; }
    }
    public class DotNetMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
