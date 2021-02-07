using CommandAPI.Data;
using Moq;
using AutoMapper;
using System;
using Xunit;
using System.Collections.Generic;
using CommandAPI.Models;
using CommandAPI.Controllers;
using CommandAPI.Profiles;
using CommandAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPITests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration config;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new();
            realProfile = new();
            config = new(c => c.AddProfile(realProfile));
            mapper = new Mapper(config);
        }

        [Fact]
        public void GetCommands_Returns200OK_IfDBIsEmpty()
        {
            VerifyType<OkObjectResult>(0);
        }

        [Fact]
        public void GetCommands_Returns200OK_IfDBHasOneResource()
        {
            VerifyType<OkObjectResult>(1);
        }

        [Fact]
        public void GetCommands_RetursCorrectType_IfDBHasOneResource()
        {
            VerifyType<ActionResult<IEnumerable<CommandGetDTO>>>(1, true);
        }

        [Fact]
        public void GetCommands_ReturnsOneItem_IfDBHasOneResource()
        {
            var result = Arrange(1);

            #region Assert
            var okResult = result.Result as OkObjectResult;
            Console.WriteLine($"HEI: {okResult.Value}");
            var commands = okResult.Value as List<CommandGetDTO>;

            Assert.Single(commands);
            #endregion
        }

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            config = null;
            mapper = null;
        }

        private void VerifyType<T>(int num, bool enumeration=false) where T: class
        {
            var arrange = Arrange(num);
            if (enumeration)
            {
                Assert.IsType<T>(arrange);
                return;
            }

            Assert.IsType<T>(arrange.Result);
        }

        private ActionResult<IEnumerable<CommandGetDTO>> Arrange(int num)
        {
            mockRepo.Setup(repo => repo.GetCommands()).Returns(GetListOfCommand(num));
            var controller = new CommandsController(mockRepo.Object, mapper);
            return controller.GetCommands();

            #region Local Function
            static List<Command> GetListOfCommand(int num)
            {
                var commands = new List<Command>();
                if (num > 0)
                {
                    commands.Add(new Command { Id = 0, Usability = "How to generate a migration", CommandLine = "dotnet ef migrations add <Name of migration>", PlatformId = 1 });
                }

                return commands;
            }
            #endregion
        }
    }
}
