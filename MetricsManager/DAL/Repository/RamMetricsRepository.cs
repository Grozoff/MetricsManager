using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public RamMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetric item)
        {
            using var connection = _connection.Connect();
            connection.Execute("INSERT INTO rammetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
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
                $"SELECT Max(time) from rammetrics where agentId = @agentId",
                new { agentId });

            return DateTimeOffset.FromUnixTimeSeconds(result);
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection
                .Query<RamMetric>(
                    $"SELECT * From rammetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = from.ToUnixTimeSeconds(),
                        toTime = to.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, int agentId)
        {
            using var connection = _connection.Connect();
            return connection
                .Query<RamMetric>(
                    $"SELECT * From rammetrics WHERE time > @FromTime AND time < @ToTime AND agentId = @agentId",
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
