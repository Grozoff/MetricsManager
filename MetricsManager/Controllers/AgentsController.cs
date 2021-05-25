using AutoMapper;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentRepository _repository;
        private readonly IMapper _mapper;

        public AgentsController(IAgentRepository repository, ILogger<AgentsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentRequest agentRequest)
        {
            _repository.Create(_mapper.Map<AgentInfo>(agentRequest));
            _logger.LogInformation("Agent successfully added to database");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            var agents = _repository.GetAll();
            var response = new AllAgentsResponse()
            {
                Agents = new List<AgentResponse>()
            };

            foreach (var agent in agents)
            {
                response.Agents.ToList().Add(new AgentResponse
                {
                    Id = agent.Id,
                    Uri = agent.Uri,                  
                });
            }
            return Ok(response);
        }
    }
}
