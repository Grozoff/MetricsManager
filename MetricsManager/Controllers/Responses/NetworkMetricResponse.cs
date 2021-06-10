using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class NetworkGetMetricsFromAgentResponse
    {
        public IEnumerable<NetworkMetricResponse> Response { get; set; }
    }

    public class NetworkGetMetricsFromClusterResponse
    {
        public IEnumerable<NetworkMetricResponse> Response { get; set; }
    }
    public class NetworkMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
