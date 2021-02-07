using CommandAPI.Models;
using System;
using Xunit;

namespace CommandAPITests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command { Usability = "Change DB", PlatformId = 2, CommandLine = "db change" };
        }

        [Fact]
        public void CanChangeUsability()
        {
            testCommand.Usability = "ILAIK";
            Assert.Equal("ILAIK", testCommand.Usability);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            testCommand.CommandLine = "HEI";
            Assert.Equal("HEI", testCommand.CommandLine);
        }

        [Fact]
        public void CanChangePlatformId()
        {
            testCommand.PlatformId = 3;
            Assert.Equal(3, testCommand.PlatformId);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}
