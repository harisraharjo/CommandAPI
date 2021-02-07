using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch;

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


        [HttpGet("{id}", Name = "GetCommand")]
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
            return CreatedAtRoute(nameof(GetCommand), new { result.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, [FromBody] CommandCreateDTO commandUpdateDTO)
        {
            var command = _repository.GetCommand(id);
            if (command is null) return NotFound();

            _mapper.Map(commandUpdateDTO, command);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, [FromBody] JsonPatchDocument<CommandCreateDTO> patchDoc)
        {
            var command = _repository.GetCommand(id);
            if (command is null) return NotFound();

            var commandToPatch = _mapper.Map<CommandCreateDTO>(command);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = _repository.GetCommand(id);
            if (command is null) return NotFound();

            _repository.Delete(command);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
