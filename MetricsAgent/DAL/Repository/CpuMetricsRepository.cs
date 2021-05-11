using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
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
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time > @fromTime) and (time < @toTime)",
                new
                {
                    fromTime = from.ToUnixTimeSeconds(),
                    toTime = to.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
