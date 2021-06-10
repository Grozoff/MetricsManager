using System;

namespace MetricsManager.Client.Request
{
    public class GetAllDotNetMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
