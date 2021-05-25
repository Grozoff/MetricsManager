using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricApiResponse> Response { get; set; }
    }
    public class CpuMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}
