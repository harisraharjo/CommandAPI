using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.DTOs
{
    public class CommandGetDTO : CommandBase, IModel
    {
        [Key]
        [Required]
        public int Id { get; init; }
    }
}
