using System;

namespace MetricsManager.Controllers.Requests
{
    public class HddMetricFromAgentRequests
    {
        public HddMetricFromAgentRequests(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            AgentId = agentId;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public HddMetricFromAgentRequests()
        {

        }
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
    public class HddMetricFromClusterRequests
    {
        public HddMetricFromClusterRequests(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public HddMetricFromClusterRequests()
        {

        }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
