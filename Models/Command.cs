using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Models
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(250)]
        public string Usability { get; set; }

        [Required]
        public string CommandLine { get; set; }

        public int PlatformId { get; set; }

        public Platform Platform { get; set; }

    }
}
