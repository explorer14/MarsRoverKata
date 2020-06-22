using FluentAssertions;
using MarsRover.Domain.RoverOperations.Entities;
using System;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverInitialisationTests
    {
        [Fact]
        public void ShouldThrowIfNoTerrainProvided()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Rover(null, 3, 2, Direction.North));
        }

        [Fact]
        public void ShouldThrowIfInitialPositionOutOfTerrain()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Rover(
                    new Terrain(5, 5),
                    startingPositionX: -100001,
                    startingPositionY: 200000,
                    Direction.North));
        }

        [Fact]
        public void ShouldSetNewPositionToStartingPosition()
        {
            var rover = new Rover(
                    new Terrain(5, 5),
                    startingPositionX: 1,
                    startingPositionY: 2,
                    Direction.North);

            var roverPosition = rover.GetCurrentPosition();

            roverPosition.CurrentX.Should().Be(1);
            roverPosition.CurrentY.Should().Be(2);
        }

        [Fact]
        public void ShouldThrowIfStartingHeadingIsInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rover(
                    new Terrain(5, 5),
                    startingPositionX: 1,
                    startingPositionY: 2,
                    startingHeading: (Direction)99));
        }

        [Fact]
        public void ShouldSetStartingHeadingAndNewHeadingAsSpecified()
        {
            var rover = new Rover(
                    new Terrain(5, 5),
                    startingPositionX: 1,
                    startingPositionY: 2,
                    Direction.South);

            var roverPosition = rover.GetCurrentPosition();

            rover.StartingHeading.Should().Be(Direction.South);
            roverPosition.CurrentHeading.Should().Be(Direction.South);
        }
    }
}