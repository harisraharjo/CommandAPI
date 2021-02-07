using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public interface ICommandAPIRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetCommands();
        Command GetCommand(int id);
        void Create(Command cmd);
        void Update(Command cmd);
        void Delete(Command cmd);
    }
}
