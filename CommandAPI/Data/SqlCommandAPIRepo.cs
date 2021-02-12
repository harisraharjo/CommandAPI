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
        public SqlCommandAPIRepo(CommandContext context) => _context = context;

        public void Create(Command cmd)
        {
            if (cmd is null) ThrowArgumentNullException(cmd);

            _context.Commands.Add(cmd);
        }

        public void Delete(Command cmd)
        {
            if (cmd is null) ThrowArgumentNullException(cmd);

            _context.Commands.Remove(cmd);
        }

        public Command GetCommand(int id) => _context.Commands.FirstOrDefault(c => c.Id == id);

        public IEnumerable<Command> GetCommands() => _context.Commands.ToList();

        public bool SaveChanges() =>  _context.SaveChanges() >= 0;

        public void Update(Command cmd)
        {
            throw new NotImplementedException();
        }

        private static void ThrowArgumentNullException(Command cmd)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
    }
}
