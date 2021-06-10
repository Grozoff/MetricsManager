using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllDotNetMetricsApiResponse
    {
        public List<CpuMetricApiResponse> Response { get; set; }
    }
    public class DotNetMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
