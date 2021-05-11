using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
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
            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE (time > @fromTime) and (time < @toTime)",
                new
                {
                    fromTime = from.ToUnixTimeSeconds(),
                    toTime = to.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
