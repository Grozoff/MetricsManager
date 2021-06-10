using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using System.Collections.Generic;
using MetricsManager.DAL.Models;
using MetricsManager.Client.Response;
using MetricsManager.Client.Request;

namespace MetricsManager.MetricsJobs.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;
        public NetworkMetricJob(INetworkMetricsRepository networkMetricsRepository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {           
            var agents = _agentRepository.GetAll();

            foreach (var agent in agents)
            {
                DateTimeOffset toTime = DateTimeOffset.UtcNow;
                DateTimeOffset fromTime = _networkMetricsRepository.GetAgentLastMetricDate(agent.Id);

                AllNetworkMetricsApiResponse allNetworkMetrics = _client.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.Uri
                });

                if (allNetworkMetrics != null)
                {
                    foreach (var metric in allNetworkMetrics.Response)
                    {
                        _networkMetricsRepository.Create(new NetworkMetric
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
