namespace MetricsManager.Controllers.Requests
{
    public class AgentRequest
    {
        public AgentRequest(string uri)
        {
            Uri = uri;
        }

        public AgentRequest()
        {

        }
        public string Uri{ get; set; }       
    }
}
