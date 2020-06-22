using MarsRover.Domain.RoverOperations.Entities;

namespace MarsRover.Domain.RoverOperations.ValueTypes
{
    public readonly struct RoverInitialisationParameters
    {
        public RoverInitialisationParameters(
            int currentRoverPositionX,
            int currentRoverPositionY,
            Direction currentRoverHeading)
        {
            CurrentRoverPositionX = currentRoverPositionX;
            CurrentRoverPositionY = currentRoverPositionY;
            CurrentRoverHeading = currentRoverHeading;
        }

        public int CurrentRoverPositionX { get; }
        public int CurrentRoverPositionY { get; }
        public Direction CurrentRoverHeading { get; }
    }
}