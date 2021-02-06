using AutoMapper;
using CommandAPI.DTOs;
using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandGetDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandCreateDTO>();
        }
    }
}
