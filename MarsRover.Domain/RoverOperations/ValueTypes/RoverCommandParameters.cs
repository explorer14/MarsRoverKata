using MarsRover.Domain.RoverOperations.Entities;

namespace MarsRover.Domain.RoverOperations.ValueTypes
{
    public readonly struct RoverCommandParameters
    {
        public RoverCommandParameters(
            int terrainMaxX,
            int terrainMaxY,
            int currentRoverPositionX,
            int currentRoverPositionY,
            Direction currentRoverHeading,
            params RoverCommands[] manouvreCommands)
        {
            Terrain = new Terrain(terrainMaxX, terrainMaxY);

            RoverInitialisationParameters = new RoverInitialisationParameters(
                currentRoverPositionX,
                currentRoverPositionY,
                currentRoverHeading);

            ManouvreCommands = manouvreCommands;
        }

        public Terrain Terrain { get; }
        public RoverInitialisationParameters RoverInitialisationParameters { get; }
        public RoverCommands[] ManouvreCommands { get; }
    }
}