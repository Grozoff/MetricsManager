using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.Requests
{
    public class NetworkMetricFromAgentRequests
    {
        public NetworkMetricFromAgentRequests(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            AgentId = agentId;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public NetworkMetricFromAgentRequests()
        {

        }
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
    public class NetworkMetricFromClusterRequests
    {
        public NetworkMetricFromClusterRequests(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public NetworkMetricFromClusterRequests()
        {

        }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
