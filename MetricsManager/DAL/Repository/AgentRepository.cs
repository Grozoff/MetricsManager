using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly SQLiteConnectionFactory _connection;

        public AgentRepository(SQLiteConnectionFactory connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(AgentInfo agent)
        {
            using var connection = _connection.Connect();
            connection.Execute(
                $"INSERT INTO agents(uri) VALUES (@uri);",
                new 
                { 
                    uri = agent.Uri,                     
                });
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = _connection.Connect();
            return connection.Query<AgentInfo>($"SELECT * FROM agents").ToList();
        }

        public AgentInfo GetById(int id)
        {
            using var connection = _connection.Connect();
            return connection
                .QuerySingle<AgentInfo>("SELECT * FROM agents WHERE id = @id",
                    new
                    {
                        id
                    });
        }

        public void Update(AgentInfo agent)
        {
            using var connection = _connection.Connect();
            connection.Execute(
                $"UPDATE agents SET uri=@uri, where id=@id;",
                new
                {
                    uri = agent.Uri,
                    id = agent.Id
                });
        }
    }
}
