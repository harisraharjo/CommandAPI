using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.DTOs
{

    //public record CommandCreateDTO([Required][MaxLength(250)] string Usability, 
    //    [Required] string CommandLine, 
    //    [Required] string PlatformId);
    public class CommandCreateDTO : CommandBase
    {
        [Required]
        public override int PlatformId { get => base.PlatformId; set => base.PlatformId = value; }
    }
}
