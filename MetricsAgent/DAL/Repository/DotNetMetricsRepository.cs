using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public DotNetMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(DotNetMetric item)
        {
            using var connection = _connection.Connect();
            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                new 
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE (time > @fromTime) and (time < @toTime)",
                new
                {
                    fromTime = from.ToUnixTimeSeconds(),
                    toTime = to.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
