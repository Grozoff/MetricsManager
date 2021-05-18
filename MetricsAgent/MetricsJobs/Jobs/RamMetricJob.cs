using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private readonly PerformanceCounter _counter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new RamMetric { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
