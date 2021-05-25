using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsManager.Client;
using MetricsManager.Client.Request;
using MetricsManager.Client.Response;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Collections.Generic;

namespace MetricsManager.MetricsJobs.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;
        public RamMetricJob(IRamMetricsRepository ramMetricsRepository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {           
            var agents = _agentRepository.GetAll();

            foreach (var agent in agents)
            {
                DateTimeOffset toTime = DateTimeOffset.UtcNow;
                DateTimeOffset fromTime = _ramMetricsRepository.GetAgentLastMetricDate(agent.Id);

                AllRamMetricsApiResponse allRamMetrics = _client.GetAllRamMetrics(new GetAllRamMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.Uri
                });

                if (allRamMetrics != null)
                {
                    foreach (var metric in allRamMetrics.Response)
                    {
                        _ramMetricsRepository.Create(new RamMetric
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
