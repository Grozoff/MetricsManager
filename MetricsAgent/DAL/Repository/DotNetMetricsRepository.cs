using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public class DotNetMetricsRepository : ICpuMetricsRepository
    {
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
