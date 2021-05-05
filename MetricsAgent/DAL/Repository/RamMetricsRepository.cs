using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLliteConnection _connection;

        public RamMetricsRepository(SQLliteConnection connection)
        {
            _connection = connection;
        }

        public void Create(RamMetric item)
        {
            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();

            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics WHERE (time > @fromTime) and (time < @toTime)";
            cmd.Parameters.AddWithValue("@fromTime", fromSeconds);
            cmd.Parameters.AddWithValue("@toTime", toSeconds);
            cmd.Prepare();

            connection.Open();

            var returnList = new List<RamMetric>();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                returnList.Add(new RamMetric()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = reader.GetInt64(2)
                });
            }
            connection.Close();

            return returnList.Count > 0 ? returnList : null;
        }
    }
}
