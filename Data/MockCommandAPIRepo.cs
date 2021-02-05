using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public void Create(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Command GetCommand(int id)
        {
            return new Command {
                Id = 1, Usability = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = new Platform { Id = 1, Name = ".Net Core EF", OfficialDocs = "kokokok" } };
        }

        public IEnumerable<Command> GetCommands()
        {
            return  new List<Command> {
                new Command{
                    Id=0, Usability="How to generate a migration",
                    CommandLine="dotnet ef migrations add <Name of Migration>", PlatformId=1},
                new Command{
                    Id=1, Usability="Run Migrations",
                    CommandLine="dotnet ef database update", PlatformId=1},
                new Command{
                    Id=2, Usability="List active migrations",
                    CommandLine="dotnet ef migrations list", PlatformId=1}
            };

        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
