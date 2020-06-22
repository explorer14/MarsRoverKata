using FluentAssertions;
using MarsRover.Domain.RoverOperations.Entities;
using System;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverMovementTests
    {
        [Theory]
        [InlineData(5, 5, Direction.North)]
        [InlineData(5, 5, Direction.East)]
        [InlineData(0, 0, Direction.West)]
        [InlineData(0, 0, Direction.South)]
        [InlineData(0, 3, Direction.West)]
        [InlineData(0, 2, Direction.West)]
        [InlineData(3, 0, Direction.South)]
        [InlineData(2, 0, Direction.South)]
        [InlineData(5, 0, Direction.East)]
        [InlineData(5, 0, Direction.South)]
        [InlineData(0, 5, Direction.North)]
        [InlineData(0, 5, Direction.West)]
        public void ShouldThrowIfAskedToDriveOffTheTerrain(
            int currentPositionX,
            int currentPositionY,
            Direction currentHeading)
        {
            var rover = new Rover(
                new Terrain(5, 5),
                currentPositionX,
                currentPositionY,
                currentHeading);

            Assert.Throws<InvalidOperationException>(() => rover.Move());
        }

        [Theory]
        [InlineData(1, 2, Direction.North, 1, 3)]
        [InlineData(1, 2, Direction.East, 2, 2)]
        [InlineData(1, 2, Direction.West, 0, 2)]
        [InlineData(1, 2, Direction.South, 1, 1)]
        public void ShouldMoveByOneUnitInTheDirectionRoverIsCurrentlyHeading(
            int startingPositionX,
            int startingPositionY,
            Direction currentHeading,
            int expectedPositionX,
            int expectedPositionY)
        {
            var rover = new Rover(
                new Terrain(5, 5),
                startingPositionX,
                startingPositionY,
                currentHeading);

            var roverPosition = rover.Move();

            roverPosition.CurrentX.Should().Be(expectedPositionX);
            roverPosition.CurrentY.Should().Be(expectedPositionY);
        }
    }
}