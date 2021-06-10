using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to , int agentId);
        IList<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        DateTimeOffset GetAgentLastMetricDate(int agentId);
        void Create(T item);
    }
}
