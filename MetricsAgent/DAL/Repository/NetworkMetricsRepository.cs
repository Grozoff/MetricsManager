using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public NetworkMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetric item)
        {
            using var connection = _connection.Connect();
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time > @fromTime) and (time < @toTime)",
                new
                {
                    fromTime = from.ToUnixTimeSeconds(),
                    toTime = to.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
