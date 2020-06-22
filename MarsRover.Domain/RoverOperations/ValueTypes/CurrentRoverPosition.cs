using MarsRover.Domain.RoverOperations.Entities;
using System;

namespace MarsRover.Domain.RoverOperations.ValueTypes
{
    public readonly struct CurrentRoverPosition
    {
        public CurrentRoverPosition(
            Guid roverId,
            int currentX,
            int currentY,
            Direction currentHeading)
        {
            RoverId = roverId;
            CurrentX = currentX;
            CurrentY = currentY;
            CurrentHeading = currentHeading;
        }

        public Guid RoverId { get; }
        public int CurrentX { get; }
        public int CurrentY { get; }
        public Direction CurrentHeading { get; }

        public override string ToString() =>
            $"Rover {RoverId} moved to ({CurrentX}, {CurrentY}). Heading: {CurrentHeading}";
    }
}