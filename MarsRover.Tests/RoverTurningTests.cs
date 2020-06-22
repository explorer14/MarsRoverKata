using FluentAssertions;
using MarsRover.Domain.RoverOperations.Entities;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverTurningTests
    {
        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void ShouldTurnLeftWhenAskedTo(Direction inputHeading, Direction expectedOutputHeading)
        {
            var rover = new Rover(new Terrain(5, 5), 1, 2, inputHeading);
            var newPosition = rover.TurnLeft();
            newPosition.CurrentHeading.Should().Be(expectedOutputHeading);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void ShouldTurnRightWhenAskedTo(Direction inputHeading, Direction expectedOutputHeading)
        {
            var rover = new Rover(new Terrain(5, 5), 1, 2, inputHeading);
            var newPosition = rover.TurnRight();
            newPosition.CurrentHeading.Should().Be(expectedOutputHeading);
        }
    }
}