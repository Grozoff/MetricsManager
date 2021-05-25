using MetricsManager.Client;
using MetricsManager.Client.Request;
using MetricsManager.Client.Response;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.MetricsJobs.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;

        public DotNetMetricJob(IDotNetMetricsRepository dotNetMetricsRepository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _dotNetMetricsRepository = dotNetMetricsRepository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {           
            var agents = _agentRepository.GetAll();

            foreach (var agent in agents)
            {
                DateTimeOffset toTime = DateTimeOffset.UtcNow;
                DateTimeOffset fromTime = _dotNetMetricsRepository.GetAgentLastMetricDate(agent.Id);

                AllDotNetMetricsApiResponse allDotNetMetrics = _client.GetAllDotNetMetrics(new GetAllDotNetMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.Uri
                });

                if (allDotNetMetrics != null)
                {
                    foreach (var metric in allDotNetMetrics.Response)
                    {
                        _dotNetMetricsRepository.Create(new DotNetMetric
                        {
                            AgentId = agent.Id,
                            Time = metric.Time.ToUnixTimeSeconds(),
                            Value = metric.Value
                        });
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
