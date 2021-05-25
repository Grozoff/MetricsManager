using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllHddMetricsApiResponse
    {
        public List<CpuMetricApiResponse> Response { get; set; }
    }
    public class HddMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
        public int Id { get; set; }
    }
}
