using System;

namespace MetricsManager.Client.Request
{
    public class GetAllCpuMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
