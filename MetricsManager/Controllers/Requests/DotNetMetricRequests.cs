using System;

namespace MetricsManager.Controllers.Requests
{
    public class DotNetMetricFromAgentRequests
    {
        public DotNetMetricFromAgentRequests(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            AgentId = agentId;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public DotNetMetricFromAgentRequests()
        {

        }
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
    public class DotNetMetricFromClusterRequests
    {
        public DotNetMetricFromClusterRequests(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public DotNetMetricFromClusterRequests()
        {

        }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
