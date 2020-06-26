using FluentAssertions;
using MarsRover.ConsoleApp;
using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.Usecases;
using MarsRover.Domain.RoverOperations.ValueTypes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Tests.IntegratedTests
{
    public class WhenControllingRoverViaConsole
    {
        [Fact]
        public async Task ShouldProcessCommandsAndMoveRover()
        {
            var testTransmitter = new TestTransmitter();
            var useCase = new RoverOperationUsecase(
                new CommandLineInputParser(new[] { "5 5", "1 2 N", "LML" }),
                testTransmitter);

            // even though the rover will have undergone intermediate positions,
            // I only need to assert on the final position. If that's correct,
            // then the rover obviously made the right movements to end up at the final position
            var expectedFinalPositionOfRover =
                new CurrentRoverPosition(Guid.NewGuid(), 0, 2, Direction.South);

            await useCase.StartRoverOperation();

            testTransmitter.FinalRoverPosition
                .CurrentX.Should().Be(expectedFinalPositionOfRover.CurrentX);
            testTransmitter.FinalRoverPosition
                .CurrentY.Should().Be(expectedFinalPositionOfRover.CurrentY);
            testTransmitter.FinalRoverPosition
                .CurrentHeading.Should().Be(expectedFinalPositionOfRover.CurrentHeading);
        }

        [Fact]
        public void ShouldProcessCommandsAndMoveRover2()
        {
            var testTransmitter = new TestTransmitter();

            Program.TransmitterToUse = testTransmitter;
            Program.Main(new[] { "5 5", "1 2 N", "LML" });

            var expectedFinalPositionOfRover =
                new CurrentRoverPosition(Guid.NewGuid(), 0, 2, Direction.South);

            testTransmitter.FinalRoverPosition
                .CurrentX.Should().Be(expectedFinalPositionOfRover.CurrentX);
            testTransmitter.FinalRoverPosition
                .CurrentY.Should().Be(expectedFinalPositionOfRover.CurrentY);
            testTransmitter.FinalRoverPosition
                .CurrentHeading.Should().Be(expectedFinalPositionOfRover.CurrentHeading);
        }
    }

    internal class TestTransmitter : ITransmitRoverPosition
    {
        public CurrentRoverPosition FinalRoverPosition { get; private set; }

        public Task Transmit(CurrentRoverPosition currentRoverPosition)
        {
            FinalRoverPosition = currentRoverPosition;

            return Task.CompletedTask;
        }
    }
}