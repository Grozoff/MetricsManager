using System;

namespace MetricsManager.Client.Request
{
    public class GetAllNetworkMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
