using MarsRover.Domain.RoverOperations.Entities;
using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.ConsoleApp
{
    public class CommandLineInputParser : IRetrieveRoverCommands
    {
        private static readonly string terrainPattern = @"^\d+\s\d+$";
        private static readonly string roverPosPattern = @"^\d+\s\d+\s(N|n|E|e|W|w|S|s){1}$";
        private static readonly string movementSeqPattern = @"^(L|l|M|m|R|r)+$";
        private readonly string[] commandLineArgs;

        public CommandLineInputParser(string[] commandLineArgs)
        {
            Validate(commandLineArgs);

            this.commandLineArgs = commandLineArgs;
        }

        public Task<IReadOnlyCollection<RoverCommandParameters>> GetAll()
        {
            var commandParameters = new List<RoverCommandParameters>();
            var terrain = GetTerrain(commandLineArgs[0]);

            for (var i = 1; i < commandLineArgs.Length;)
            {
                // set the initial and new positions
                (int X, int Y, string Heading) initialRoverPosition
                    = GetInitialRoverPosition(commandLineArgs[i], terrain);

                // load instruction sequence
                var commands = GetMovementSequence(commandLineArgs[i + 1]);

                commandParameters.Add(
                    new RoverCommandParameters(
                        terrain.MaxX,
                        terrain.MaxY,
                        initialRoverPosition.X,
                        initialRoverPosition.Y,
                        MapToDirection(initialRoverPosition.Heading),
                        commands));

                i += 2;
            }

            return Task.FromResult<IReadOnlyCollection<RoverCommandParameters>>(
                commandParameters);
        }

        private void Validate(string[] commandLineArgs)
        {
            if (commandLineArgs == null || !commandLineArgs.Any())
                throw new ArgumentNullException(
                    nameof(commandLineArgs),
                    "Please provide input to control the rover.");

            if (commandLineArgs.Length < 3)
                throw new ArgumentOutOfRangeException(
                    nameof(commandLineArgs),
                    "Insufficient arguments provided. " +
                    "You must provide terrain coordinates and one or more rover positions and command sequences");
        }

        private static Terrain GetTerrain(string terrainCoords)
        {
            if (!Regex.IsMatch(terrainCoords, terrainPattern))
                throw new ArgumentOutOfRangeException(
                    nameof(terrainCoords),
                    "Invalid terrain command. Syntax is x<whitespace>y and only positive coordinates");

            var terrain = Regex.Split(terrainCoords, @"\s");

            return new Terrain(int.Parse(terrain[0]), int.Parse(terrain[1]));
        }

        private static (int initialX, int initialY, string initialHeading) GetInitialRoverPosition(
            string inputRoverPosition,
            Terrain terrain)
        {
            EnsureRoverPositionIsValid(inputRoverPosition);

            (int X, int Y, string Heading) initialRoverPosition = GetInitialRoverPositionFromInput(
                inputRoverPosition);

            EnsureInitialRoverPositionWithinTerrain(
                terrain, initialRoverPosition);

            return initialRoverPosition;
        }

        private static (int X, int Y, string Heading) GetInitialRoverPositionFromInput(
            string inputRoverPosition)
        {
            var roverPos = Regex.Split(inputRoverPosition, @"\s");

            (int X, int Y, string Heading) initialRoverPosition =
                (int.Parse(roverPos[0]), int.Parse(roverPos[1]), roverPos[2].ToUpper());

            return initialRoverPosition;
        }

        private static void EnsureInitialRoverPositionWithinTerrain(
            Terrain terrain, 
            (int X, int Y, string Heading) initialRoverPosition)
        {
            // directions
            // Assumptions:
            // a. rover never moves diagonally, only along one axis
            // b. therefore only 4 directions possible N, E, W, S
            // ensure rover starts inside the plateau or just on the edge
            if (initialRoverPosition.X > terrain.MaxX ||
                initialRoverPosition.Y > terrain.MaxY ||
                initialRoverPosition.X < terrain.MinX ||
                initialRoverPosition.Y < terrain.MinY)
                throw new ArgumentOutOfRangeException(
                    nameof(initialRoverPosition),
                    $"Rover cannot start outside the terrain" +
                    $" @ ({initialRoverPosition.X},{initialRoverPosition.Y})");
        }

        private static void EnsureRoverPositionIsValid(string roverPosition)
        {
            if (!Regex.IsMatch(roverPosition, roverPosPattern))
                throw new ArgumentOutOfRangeException(
                    nameof(roverPosition),
                    "Rover position not valid! Syntax is x<whitespace>y<whitspace>N|E|W|S and only positive coordinates");
        }

        private RoverCommands[] GetMovementSequence(string commandSequenceString)
        {
            List<RoverCommands> commands = new List<RoverCommands>();

            foreach (var character in commandSequenceString.ToLower())
            {
                // Assumption: if a command letter is not recognised,
                // I'll simply skip it and execute the rest
                if (Regex.IsMatch(character.ToString(), movementSeqPattern))
                {
                    switch (character)
                    {
                        case 'l':
                            commands.Add(RoverCommands.TurnLeft);
                            break;

                        case 'r':
                            commands.Add(RoverCommands.TurnRight);
                            break;

                        case 'm':
                            commands.Add(RoverCommands.Move);
                            break;

                        default:
                            throw new ArgumentException(
                                nameof(character), 
                                "Unrecognised movement command");
                    }
                }
            }

            return commands.ToArray();
        }

        private Direction MapToDirection(string str)
        {
            if (str.ToLower() == "n")
                return Direction.North;
            else if (str.ToLower() == "e")
                return Direction.East;
            else if (str.ToLower() == "w")
                return Direction.West;
            else
                return Direction.South;
        }
    }
}