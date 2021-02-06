using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Models
{
    public abstract class CommandBase
    {
        [Required]
        [MaxLength(250)]
        public string Usability { get; set; }

        [Required]
        public string CommandLine { get; set; }

        public virtual int PlatformId { get; set; }
    }
}
