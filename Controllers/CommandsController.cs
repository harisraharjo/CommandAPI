using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        public CommandsController(ICommandAPIRepo repository) => _repository = repository;


        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(int id)
        {
            var command = _repository.GetCommand(id);
            if (command is null) return NotFound();

            return Ok(command);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            var commands = _repository.GetCommands();
            return Ok(commands);
        }
    }
}
