using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("IPsec Connections", "Total Bytes Out since start");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new NetworkMetric { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
