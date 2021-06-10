using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public CpuMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using var connection = _connection.Connect();
            connection.Execute("INSERT INTO cpumetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = item.Value,
                    time = item.Time
                });
        }

        public DateTimeOffset GetAgentLastMetricDate(int agentId)
        {
            using var connection = _connection.Connect();
            var result = connection.ExecuteScalar<long>(
                $"SELECT Max(time) from cpumetrics where agentId = @agentId",
                new { agentId });

            return DateTimeOffset.FromUnixTimeSeconds(result);
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection
                .Query<CpuMetric>(
                    $"SELECT * From cpumetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = from.ToUnixTimeSeconds(),
                        toTime = to.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, int agentId)
        {
            using var connection = _connection.Connect();
            return connection
                .Query<CpuMetric>(
                    $"SELECT * From cpumetrics WHERE time > @FromTime AND time < @ToTime AND agentId = @agentId",
                    new
                    {
                        fromTime = from.ToUnixTimeSeconds(),
                        toTime = to.ToUnixTimeSeconds(),
                        agentId
                    })
                .ToList();
        }
    }
}
