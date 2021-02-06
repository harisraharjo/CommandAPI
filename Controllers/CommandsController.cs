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


        [HttpGet("{id}", Name ="GetCommand")]
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

        [HttpPost]
        public ActionResult<CommandGetDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var command = _mapper.Map<Command>(commandCreateDTO);
            _repository.Create(command);
            _repository.SaveChanges();

            var result = _mapper.Map<CommandGetDTO>(command);
            return CreatedAtRoute(nameof(GetCommand),new { Id=result.Id }, result);
        }
    }
}
