using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.DTOs;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

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

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            config = null;
            mapper = null;
        }

        #region Tests
        #region GetCommands
        [Fact]
        public void GetCommands_Returns200OK_WhenDBIsEmpty()
        {
            var result = GetCommands(0).Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCommands_Returns200OK_WhenDBHasOneResource()
        {
            var result = GetCommands(1).Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCommands_RetursCorrectType_WhenDBHasOneResource()
        {
            var result = GetCommands(1);
            Assert.IsType<ActionResult<IEnumerable<CommandGetDTO>>>(result);
        }

        [Fact]
        public void GetCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            var result = GetCommands(1);
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandGetDTO>;

            Assert.Single(commands);
        }
        #endregion

        #region GetCommand
        [Fact]
        public void GetCommand_Returns200OK__WhenValidIDProvided()
        {
            var controller = ArrangeCommand(1);
            var result = controller.GetCommand(1).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCommand_ReturnsCorrectType__WhenValidIDProvided()
        {
            var controller = ArrangeCommand(1);
            var result = controller.GetCommand(1);

            Assert.IsType<ActionResult<CommandGetDTO>>(result);
        }

        [Fact]
        public void GetCommand_Returns404NotFound_WhenNonExistentIDProvided()
        {
            var controller = ArrangeCommand(0, false);
            var result = controller.GetCommand(1).Result;

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region CreateCommand
        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            var controller = ArrangeCommand(1);
            var result = controller.CreateCommand(new());

            Assert.IsType<ActionResult<CommandGetDTO>>(result);
        }
        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            var controller = ArrangeCommand(1);
            var result = controller.CreateCommand(new()).Result;

            Assert.IsType<CreatedAtRouteResult>(result);
        }
        #endregion

        #region UpdateCommand
        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            var controller = ArrangeCommand(1);
            var result = controller.UpdateCommand(1, new());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            var controller = ArrangeCommand(0, false);
            var result = controller.UpdateCommand(0, new());

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region PartialUpdateCommand / PatchCommand
        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            var controller = ArrangeCommand(0, false);
            var result = controller.PartialCommandUpdate(0, new());

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region DeleteCommand
        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            var controller = ArrangeCommand(1);
            var result = controller.DeleteCommand(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            var controller = ArrangeCommand(1, false);
            var result = controller.DeleteCommand(0);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion
        #endregion

        #region Method
        private CommandsController ArrangeCommand(int id, bool mockInstance=true)
        {
            var cmd = mockInstance ? new Command
            {
                Id = 1,
                Usability = "mock",
                PlatformId = 1,
                CommandLine = "Mock"
            } : null;

            return ArrangeController(repo => repo.GetCommand(id), () => cmd);
        }

        private ActionResult<IEnumerable<CommandGetDTO>> GetCommands(int num)
        {
            var controller = ArrangeController(repo => repo.GetCommands(), () => GetListOfCommand(num));
            return controller.GetCommands();

            static List<Command> GetListOfCommand(int num)
            {
                var commands = new List<Command>();
                if (num > 0)
                {
                    commands.Add(new Command { Id = 0, Usability = "How to generate a migration", CommandLine = "dotnet ef migrations add <Name of migration>", PlatformId = 1 });
                }

                return commands;
            }
        }

        private CommandsController ArrangeController<T, Y>(Expression<Func<ICommandAPIRepo, T>> expression, Func<Y> val)
        {
            mockRepo.Setup(expression).Returns(val);
            return new CommandsController(mockRepo.Object, mapper);
        }
        #endregion
    }
}
