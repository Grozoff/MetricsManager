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
        private SQLiteConnection _connection;
        public void Create(CpuMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            throw new NotImplementedException();
        }
    }
}
