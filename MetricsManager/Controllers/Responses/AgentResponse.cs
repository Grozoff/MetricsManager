using System.Collections.Generic;

namespace MetricsManager.Controllers.Responses
{
    public class AllAgentsResponse
    {
        public IEnumerable<AgentResponse> Agents { get; set; }
    }
    public class AgentResponse
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public bool Status { get; set; }
    }
}
