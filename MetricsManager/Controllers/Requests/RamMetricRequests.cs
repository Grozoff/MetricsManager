using System;

namespace MetricsManager.Controllers.Requests
{
    public class RamMetricFromAgentRequests
    {
        public RamMetricFromAgentRequests(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            AgentId = agentId;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public RamMetricFromAgentRequests()
        {

        }
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
    public class RamMetricFromClusterRequests
    {
        public RamMetricFromClusterRequests(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public RamMetricFromClusterRequests()
        {

        }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
