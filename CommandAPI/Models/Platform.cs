using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Models
{
    public class Platform : IModel
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OfficialDocs { get; set; }

        public ICollection<Command> Commands { get; set; }
    }
}
