using System;

namespace MarsRover.Domain.RoverOperations.Entities
{
    /// <summary>
    /// In this problem, I am assuming a simple 2D Terrain that has its origin at (0,0) on the
    /// bottom left from the frame of reference of, say, a satellite camera orbiting around Mars.
    /// Therefore I am not catering for negative coordinates
    /// </summary>
    public sealed class Terrain
    {
        private const int ORIGIN = 0;

        public Terrain(int maxX, int maxY)
        {
            if (maxX < ORIGIN || maxY < ORIGIN)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(maxX)}, {nameof(maxY)}",
                    $"This terrain can only contain " +
                    $"positive co-ordinatesm with origin at ({ORIGIN},{ORIGIN})");

            MaxX = maxX;
            MaxY = maxY;
            MinX = ORIGIN;
            MinY = ORIGIN;
        }

        public int MaxX { get; }
        public int MaxY { get; }
        public int MinX { get; }
        public int MinY { get; }
    }
}