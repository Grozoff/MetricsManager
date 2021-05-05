using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.Repository
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLliteConnection _connection;

        public CpuMetricsRepository(SQLliteConnection connection)
        {
            _connection = connection;
        }

        /*
         * private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
         * Вынесено в метод .Connect()
         */

        public void Create(CpuMetric item)
        {
            
            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();

            using var connection = _connection.Connect();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE (time > @fromTime) and (time < @toTime)";
            cmd.Parameters.AddWithValue("@fromTime", fromSeconds);
            cmd.Parameters.AddWithValue("@toTime", toSeconds);
            cmd.Prepare();

            connection.Open();

            var returnList = new List<CpuMetric>();
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                returnList.Add(new CpuMetric()
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
