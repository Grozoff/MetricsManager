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
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;
        public HddMetricJob(IHddMetricsRepository hddMetricsRepository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {           
            var agents = _agentRepository.GetAll();

            foreach (var agent in agents)
            {
                DateTimeOffset toTime = DateTimeOffset.UtcNow;
                DateTimeOffset fromTime = _hddMetricsRepository.GetAgentLastMetricDate(agent.Id);

                AllHddMetricsApiResponse allHddMetrics = _client.GetAllHddMetrics(new GetAllHddMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.Uri
                });

                if (allHddMetrics != null)
                {
                    foreach (var metric in allHddMetrics.Response)
                    {
                        _hddMetricsRepository.Create(new HddMetric
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
