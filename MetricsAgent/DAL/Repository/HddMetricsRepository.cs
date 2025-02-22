﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public HddMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetric item)
        {
            using var connection = _connection.Connect();
            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new 
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _connection.Connect();
            return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time > @fromTime) and (time < @toTime)",
                new
                {
                    fromTime = from.ToUnixTimeSeconds(),
                    toTime = to.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
