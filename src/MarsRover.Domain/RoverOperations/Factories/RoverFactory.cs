using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.ValueTypes;

namespace MarsRover.Domain.RoverOperations.Factories
{
    public static class RoverFactory
    {
        public static Rover CreateWith(RoverCommandParameters roverCommandParameters) =>
            new Rover(
                    roverCommandParameters.Terrain,
                    roverCommandParameters.RoverInitialisationParameters.CurrentRoverPositionX,
                    roverCommandParameters.RoverInitialisationParameters.CurrentRoverPositionY,
                    roverCommandParameters.RoverInitialisationParameters.CurrentRoverHeading);
    }
}