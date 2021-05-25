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
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMetricsAgentClient _client;

        public CpuMetricJob(ICpuMetricsRepository cpuMetricsRepository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAll();          
            
            foreach (var agent in agents)
            {
                DateTimeOffset toTime = DateTimeOffset.UtcNow;
                DateTimeOffset fromTime = _cpuMetricsRepository.GetAgentLastMetricDate(agent.Id);

                AllCpuMetricsApiResponse allCpuMetrics = _client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    AgentUrl = agent.Uri
                });

                if (allCpuMetrics != null)
                {
                    foreach (var metric in allCpuMetrics.Response)
                    {
                        _cpuMetricsRepository.Create(new CpuMetric
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
