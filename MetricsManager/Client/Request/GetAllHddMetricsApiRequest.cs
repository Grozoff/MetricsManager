using System;

namespace MetricsManager.Client.Request
{
    public class GetAllHddMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
