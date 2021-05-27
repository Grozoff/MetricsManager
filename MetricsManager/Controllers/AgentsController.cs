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

        /// <summary>
        /// Регистрация нового агента сбора метрик
        /// </summary>
        /// <param name="agentRequest">Содержит Uri агента</param>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">Eсли передали не правильные параметры</response>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentRequest agentRequest)
        {
            _repository.Create(_mapper.Map<AgentInfo>(agentRequest));
            _logger.LogInformation("Agent successfully added to database");
            return Ok();
        }

        /// <summary>
        /// Активация агента по его Id (не реализовано)
        /// </summary>
        /// <param name="agentId"></param>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        /// <summary>
        /// Деактивация агента по его Id (не реализовано)
        /// </summary>
        /// <param name="agentId"></param>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        /// <summary>
        /// Получение всех зарегистрированных агентов
        /// </summary>
        [HttpGet("all")]
        public IActionResult GetAllRegisteredAgents()
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
