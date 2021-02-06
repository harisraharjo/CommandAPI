using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.DTOs
{
    public record CommandGetDTO(int Id, string Usability, int PlatformId,string CommandLine);
}
