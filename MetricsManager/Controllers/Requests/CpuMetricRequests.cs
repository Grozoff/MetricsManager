using System;

namespace MetricsManager.Controllers.Requests
{
    public class CpuMetricFromAgentRequests
    {
        public CpuMetricFromAgentRequests(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            AgentId = agentId;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public CpuMetricFromAgentRequests()
        {

        }
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
    public class CpuMetricFromClusterRequests
    {
        public CpuMetricFromClusterRequests(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public CpuMetricFromClusterRequests()
        {

        }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
