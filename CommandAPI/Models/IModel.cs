using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Models
{
    public interface IModel
    {
        [Key]
        [Required]
        public int Id { get; init; }
    }
}
