using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.ValueTypes;

namespace MarsRover.Tests.Builders
{
    internal static class RoverCommandParametersBuilder
    {
        internal static RoverCommandParameters WithOneSafeMoveCommand() =>
           WithOneSafeMoveCommandWithStartingPosition(
               1, 1,
               Direction.North);

        internal static RoverCommandParameters WithOneSafeMoveCommandWithStartingPosition(
            int startingX, 
            int startingY, 
            Direction startingHeading) =>
            new RoverCommandParameters(
                3, 3, startingX, startingY,
                startingHeading,
                RoverCommands.Move);

        internal static RoverCommandParameters WithNoManouvreCommands() =>
            new RoverCommandParameters(
                    3, 3, 1, 1,
                    Direction.North);

        internal static RoverCommandParameters WithUnsafeManouvreCommand() =>
            new RoverCommandParameters(
                    3, 3, 3, 3,
                    Direction.North,
                    RoverCommands.Move);
    }
}