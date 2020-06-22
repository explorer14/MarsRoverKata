using FluentAssertions;
using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.Usecases;
using MarsRover.Domain.RoverOperations.ValueTypes;
using MarsRover.Tests.Builders;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverOperationUsecaseTests
    {
        [Fact]
        public async Task ShouldMoveRoverAndTransmitPosition()
        {
            var startingX = 1;
            var startingY = 1;
            var startingHeading = Direction.North;

            Mock<IRetrieveRoverCommands> commandRetrieverStub = CreateCommandRetrieverStubWith(
                startingX, startingY, startingHeading);

            var positionProvidedToTransmitter = default(CurrentRoverPosition);
            var transmitterStub = CreateRoverPositionTransmitterStubToInvoke(
                currentPosition => positionProvidedToTransmitter = currentPosition);

            var useCase = new RoverOperationUsecase(
                commandRetrieverStub.Object,
                transmitterStub.Object);

            await useCase.StartRoverOperation();
            positionProvidedToTransmitter.Should().NotBeNull();
            positionProvidedToTransmitter.CurrentY.Should().Be(startingY + 1);
            positionProvidedToTransmitter.CurrentX.Should().Be(startingX);
            positionProvidedToTransmitter.CurrentHeading.Should().Be(startingHeading);
        }

        [Fact]
        public async Task ShouldProcessCommandsAndTransmitCurrentPosition()
        {
            var manouvreCommandParameter = RoverCommandParametersBuilder.WithOneSafeMoveCommand();

            var commandRetrieverStub = new Mock<IRetrieveRoverCommands>();
            commandRetrieverStub
                .Setup(x => x.GetAll())
                .ReturnsAsync(new[]
                {
                    manouvreCommandParameter
                })
                .Verifiable();

            var transmitterStub = new Mock<ITransmitRoverPosition>();
            transmitterStub
                .Setup(x => x.Transmit(It.IsAny<CurrentRoverPosition>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var useCase = new RoverOperationUsecase(
                commandRetrieverStub.Object,
                transmitterStub.Object);

            await useCase.StartRoverOperation();

            commandRetrieverStub.Verify(x => x.GetAll(), Times.Once);
            transmitterStub.Verify(x =>
                x.Transmit(It.IsAny<CurrentRoverPosition>()),
                Times.Once);
        }

        [Fact]
        public async Task ShouldThrowIfCommandForcesRoverToMoveOffTerrain()
        {
            var commandRetrieverStub = new Mock<IRetrieveRoverCommands>();
            commandRetrieverStub
                .Setup(x => x.GetAll())
                .ReturnsAsync(new[]
                {
                    RoverCommandParametersBuilder.WithUnsafeManouvreCommand()
                });            

            var useCase = new RoverOperationUsecase(
                commandRetrieverStub.Object,
                new Mock<ITransmitRoverPosition>().Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.StartRoverOperation());
        }

        [Fact]
        public async Task ShouldThrowIfThereAreNoCommandsToProcess()
        {
            var commandRetrieverStub = new Mock<IRetrieveRoverCommands>();
            commandRetrieverStub
                .Setup(x => x.GetAll())
                .ReturnsAsync(Array.Empty<RoverCommandParameters>());

            var useCase = new RoverOperationUsecase(
                commandRetrieverStub.Object,
                new Mock<ITransmitRoverPosition>().Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => useCase.StartRoverOperation());
        }

        [Fact]
        public async Task ShouldNotTransmitPositionIfThereAreNoManouvreCommandsToProcess()
        {
            var commandRetrieverStub = new Mock<IRetrieveRoverCommands>();
            commandRetrieverStub
                .Setup(x => x.GetAll())
                .ReturnsAsync(new[]
                {
                    RoverCommandParametersBuilder.WithNoManouvreCommands()
                });

            var transmitterStub = new Mock<ITransmitRoverPosition>();
            transmitterStub
                .Setup(x => x.Transmit(It.IsAny<CurrentRoverPosition>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var useCase = new RoverOperationUsecase(
                commandRetrieverStub.Object,
                transmitterStub.Object);

            await useCase.StartRoverOperation();
            transmitterStub.Verify(x => x.Transmit(It.IsAny<CurrentRoverPosition>()), Times.Never);
        }

        private static Mock<ITransmitRoverPosition> CreateRoverPositionTransmitterStubToInvoke(
            Action<CurrentRoverPosition> action)
        {
            var transmitterStub = new Mock<ITransmitRoverPosition>();
            transmitterStub
                .Setup(x => x.Transmit(It.IsAny<CurrentRoverPosition>()))
                .Callback(action)
                .Returns(Task.CompletedTask);

            return transmitterStub;
        }

        private static Mock<IRetrieveRoverCommands> CreateCommandRetrieverStubWith(
            int startingX,
            int startingY,
            Direction startingHeading)
        {
            var manouvreCommandParameter = RoverCommandParametersBuilder.WithOneSafeMoveCommandWithStartingPosition(
                            startingX, startingY, startingHeading);

            var commandRetrieverStub = new Mock<IRetrieveRoverCommands>();
            commandRetrieverStub
                .Setup(x => x.GetAll())
                .ReturnsAsync(new[]
                {
                    manouvreCommandParameter
                });
            return commandRetrieverStub;
        }
    }
}