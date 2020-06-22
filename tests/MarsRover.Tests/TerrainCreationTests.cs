using MarsRover.Domain.RoverOperations.Entities;
using System;
using Xunit;

namespace MarsRover.Tests
{
    public class TerrainCreationTests
    {
        [Fact]
        public void ShouldThrowIfTerrainOutOfBounds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Terrain(-10000, -int.MaxValue));
        }
    }
}