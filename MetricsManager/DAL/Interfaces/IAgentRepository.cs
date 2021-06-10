using MetricsManager.DAL.Models;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository
    {
        void Create(AgentInfo agent);
        IList<AgentInfo> GetAll();
        AgentInfo GetById(int id);
        void Update(AgentInfo agent);
    }
}
