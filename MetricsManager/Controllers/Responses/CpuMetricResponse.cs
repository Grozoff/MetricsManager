using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class CpuGetMetricsFromAgentResponse
    {
        public IEnumerable<CpuMetricResponse> Response { get; set; }
    }

    public class CpuGetMetricsFromClusterResponse
    {
        public IEnumerable<CpuMetricResponse> Response { get; set; }
    }
    public class CpuMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}
