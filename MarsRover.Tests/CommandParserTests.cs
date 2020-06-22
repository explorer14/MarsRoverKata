using FluentAssertions;
using MarsRover.ConsoleApp;
using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.ValueTypes;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Tests
{
    public class CommandParserTests
    {
        [Fact]
        public void ShouldThrowIfCommandInputEmpty()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CommandLineInputParser(null));
        }

        [Fact]
        public void ShouldThrowIfInsufficientArgumentsProvided()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new CommandLineInputParser(new string[2]));
        }

        [Theory]
        [InlineData("-12121 -5345345", "1 2 n", "lml")]
        [InlineData("12121 5345345", "1 2 X", "lml")]
        public async Task ShouldThrowIfCommandsInvalid(params string[] badCommandLineArgs)
        {
            var parser = new CommandLineInputParser(badCommandLineArgs);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => parser.GetAll());
        }

        [Fact]
        public async Task ShouldFilterOutInvalidMovementCommands()
        {
            var invalidCommand = "x";
            var validCommands = "lm";
            var parser = new CommandLineInputParser(
                new[] { "5 5", "1 2 n", $"{invalidCommand}{validCommands}" });
            var commandParameters = await parser.GetAll();
            commandParameters.First().ManouvreCommands
                .Should()
                .HaveCount(2);
            commandParameters.First().ManouvreCommands
                .Should()
                .ContainInOrder(
                    RoverCommands.TurnLeft,
                    RoverCommands.Move);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(-6, -5)]
        public async Task ShouldThrowIfRoverInitialPositionIsOutsideTerrain(
            params int[] roverPos)
        {
            var parser = new CommandLineInputParser(
                new[] { "5 5", $"{roverPos[0]} {roverPos[1]} n", "lmx" });

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => parser.GetAll());
        }

        [Fact]
        public async Task ShouldReturnMultipleCommandParametersForMultiRoverCommandInput()
        {
            var parser = new CommandLineInputParser(
                new[] { "5 5", "1 2 n", "lml", "3 3 e", "mmr" });

            var commands = await parser.GetAll();
            commands.Should().HaveCount(2);

            var command = commands.First();
            command.Terrain.MaxX.Should().Be(5);
            command.Terrain.MaxY.Should().Be(5);

            command.RoverInitialisationParameters.CurrentRoverPositionX.Should().Be(1);
            command.RoverInitialisationParameters.CurrentRoverPositionY.Should().Be(2);
            command.RoverInitialisationParameters.CurrentRoverHeading.Should().Be(Direction.North);

            command.ManouvreCommands.Count().Should().Be(3);
            command.ManouvreCommands.Should()
                .ContainInOrder(
                    RoverCommands.TurnLeft,
                    RoverCommands.Move,
                    RoverCommands.TurnLeft);

            var command2 = commands.Last();

            command2.Terrain.MaxX.Should().Be(5);
            command2.Terrain.MaxY.Should().Be(5);

            command2.RoverInitialisationParameters.CurrentRoverPositionX.Should().Be(3);
            command2.RoverInitialisationParameters.CurrentRoverPositionY.Should().Be(3);
            command2.RoverInitialisationParameters.CurrentRoverHeading.Should().Be(Direction.East);

            command2.ManouvreCommands.Count().Should().Be(3);
            command2.ManouvreCommands.Should()
                .ContainInOrder(
                    RoverCommands.Move,
                    RoverCommands.Move,
                    RoverCommands.TurnRight);
        }
    }
}