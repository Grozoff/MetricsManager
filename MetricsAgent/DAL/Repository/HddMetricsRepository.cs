using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repository
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public HddMetricsRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
        }

        public void Create(HddMetric item)
        {
            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();

            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (time > @fromTime) and (time < @toTime)";
            cmd.Parameters.AddWithValue("@fromTime", fromSeconds);
            cmd.Parameters.AddWithValue("@toTime", toSeconds);
            cmd.Prepare();

            connection.Open();

            var returnList = new List<HddMetric>();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                returnList.Add(new HddMetric()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = reader.GetInt64(2)
                });
            }
            connection.Close();

            return returnList;
        }
    }
}
