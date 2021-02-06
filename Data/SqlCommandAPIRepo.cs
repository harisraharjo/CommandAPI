using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {
        private readonly CommandContext _context;
        public SqlCommandAPIRepo(CommandContext context)
        {
            _context = context;
        }

        public void Create(Command cmd)
        {
            if (cmd is null) throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Add(cmd);
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Command GetCommand(int id)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Command> GetCommands()
        {
            return _context.Commands.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
