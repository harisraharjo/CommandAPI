using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.DTOs;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public ActionResult<CommandGetDTO> GetCommand(int id)
        {
            var command = _repository.GetCommand(id);
            if (command is null) return NotFound();

            var result = _mapper.Map<CommandGetDTO>(command);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandGetDTO>> GetCommands()
        {
            var commands = _repository.GetCommands();
            var result = _mapper.Map<IEnumerable<CommandGetDTO>>(commands);
            return Ok(result);
        }
    }
}
