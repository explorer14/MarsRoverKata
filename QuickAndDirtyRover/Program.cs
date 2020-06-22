using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuickAndDirtyRover
{
    internal class Program
    {
        private static readonly string terrainPattern = @"^\d+\s\d+$";
        private static readonly string roverPosPattern = @"^\d+\s\d+\s(N|n|E|e|W|w|S|s){1}$";
        private static readonly string movementSeqPattern = @"^(L|l|M|m|R|r)+$";

        private static void Main(string[] args)
        {
            try
            {
                ValidateArguments(args);

                // establish bounds
                // Assumptions
                // a. 0,0 as the origin on the bottom left corner
                // b. only positive translation, i.e. rover can only back-track
                // by a series of turns and the moving, it cannot reverse
                (int minX, int minY, int maxX, int maxY) terrain = GetTerrainBounds(args[0]);

                for (var i = 1; i < args.Length;)
                {
                    // set the initial and new positions
                    (int X, int Y, char Heading) initialRoverPosition
                        = GetInitialRoverPosition(args[i], terrain);

                    (int X, int Y, char Heading) newRoverPosition = initialRoverPosition;

                    // load instruction sequence
                    char[] instructions = GetMovementSequence(args[i + 1]);

                    Console.WriteLine($"Rover starting @ {initialRoverPosition}");
                    newRoverPosition = ExecuteMovementSequence(terrain, instructions, newRoverPosition);
                    i += 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static (int X, int Y, char Heading) ExecuteMovementSequence(
            (int minX, int minY, int maxX, int maxY) terrain,
            char[] instructions,
            (int X, int Y, char Heading) newRoverPosition)
        {
            // execute instruction one by one and record rover's
            // current position ensuring it stays within the plateau's bounds
            foreach (var instruction in instructions)
            {
                try
                {
                    switch (instruction)
                    {
                        case 'L':
                            newRoverPosition.Heading = TurnLeft(newRoverPosition.Heading);
                            Console.WriteLine($"Rover turned LEFT now @ {newRoverPosition}");
                            break;

                        case 'M':
                            newRoverPosition = Move(terrain, newRoverPosition);
                            Console.WriteLine($"Rover MOVED, now @ {newRoverPosition}");
                            break;

                        case 'R':
                            newRoverPosition.Heading = TurnRight(newRoverPosition.Heading);
                            Console.WriteLine($"Rover turned RIGHT now @ {newRoverPosition}");
                            break;

                        default:
                            Console.WriteLine("Invalid command!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} \n Cannot dive off the cliff!");
                }
            }

            return newRoverPosition;
        }

        private static void ValidateArguments(string[] args)
        {
            if (args == null || !args.Any())
                throw new ArgumentException(
                    nameof(args),
                    "Please provide input to control the rover.");

            if (args.Length < 3)
                throw new ArgumentOutOfRangeException(
                    nameof(args),
                    "Insufficient arguments provided. " +
                    "You must provide terrain coordinates and one or more rover positions and command sequences");
        }

        private static char[] GetMovementSequence(string movementSequence)
        {
            if (!Regex.IsMatch(movementSequence, movementSeqPattern))
                throw new ArgumentException(
                    nameof(movementSequence),
                    "Movement sequence not valid! Syntax is a combination of L,M,Rs");

            return movementSequence.ToUpper().ToCharArray();
        }

        private static (int initialX, int initialY, char initialHeading) GetInitialRoverPosition(
            string roverPosition,
            (int minX, int minY, int maxX, int maxY) terrain)
        {
            if (!Regex.IsMatch(roverPosition, roverPosPattern))
                throw new ArgumentException(
                    nameof(roverPosition),
                    "Rover position not valid! Syntax is x y N|E|W|S and only positive coordinates");

            var roverPos = Regex.Split(roverPosition, @"\s");

            (int X, int Y, char Heading) initialRoverPosition =
                (int.Parse(roverPos[0]), int.Parse(roverPos[1]), char.Parse(roverPos[2].ToUpper()));

            // directions
            // Assumptions:
            // a. rover never moves diagonally, only along one axis
            // b. therefore only 4 directions possible N, E, W, S
            // ensure rover starts inside the plateau or just on the edge
            if (initialRoverPosition.X > terrain.maxX ||
                initialRoverPosition.Y > terrain.maxY ||
                initialRoverPosition.X < terrain.minX ||
                initialRoverPosition.Y < terrain.minY)
                throw new ArgumentOutOfRangeException(
                    nameof(initialRoverPosition),
                    $"Rover cannot start outside the plateau" +
                    $" @ ({initialRoverPosition.X},{initialRoverPosition.Y})");

            return initialRoverPosition;
        }

        private static (int minX, int minY, int maxX, int maxY) GetTerrainBounds(string terrainCoords)
        {
            if (!Regex.IsMatch(terrainCoords, terrainPattern))
                throw new ArgumentException(
                    nameof(terrainCoords),
                    "Invalid terrain command. Syntax is x y and only positive coordinates");

            var terrain = Regex.Split(terrainCoords, @"\s");

            return (0, 0, int.Parse(terrain[0]), int.Parse(terrain[1]));
        }

        private static (int newX, int newY, char newHeading) Move(
            (int minX, int minY, int maxX, int maxY) terrain,
            (int X, int Y, char Heading) currentRoverPosition)
        {
            // if the current position is on the edge, throw exception
            // else move by incrementing the current rover position by 1 unit in x or y direction
            // if heading is N-S => y axis, if it is E-W => x axis

            if (currentRoverPosition.Heading == 'N')
            {
                if (currentRoverPosition.Y + 1 > terrain.maxY)
                    throw new ArgumentOutOfRangeException(
                        nameof(currentRoverPosition),
                        $"Rover is close to the edge ({currentRoverPosition.X},{currentRoverPosition.Y})");

                currentRoverPosition.Y++;
            }

            if (currentRoverPosition.Heading == 'S')
            {
                if (currentRoverPosition.Y - 1 < terrain.minY)
                    throw new ArgumentOutOfRangeException(
                        nameof(currentRoverPosition),
                        $"Rover is close to the edge ({currentRoverPosition.X},{currentRoverPosition.Y})");

                currentRoverPosition.Y--;
            }

            if (currentRoverPosition.Heading == 'E')
            {
                if (currentRoverPosition.X + 1 > terrain.maxX)
                    throw new ArgumentOutOfRangeException(
                        nameof(currentRoverPosition),
                        $"Rover is close to the edge ({currentRoverPosition.X},{currentRoverPosition.Y})");

                currentRoverPosition.X++;
            }

            if (currentRoverPosition.Heading == 'W')
            {
                if (currentRoverPosition.X - 1 < terrain.minX)
                    throw new ArgumentOutOfRangeException(
                        nameof(currentRoverPosition),
                        $"Rover is close to the edge ({currentRoverPosition.X},{currentRoverPosition.Y})");

                currentRoverPosition.X--;
            }

            return currentRoverPosition;
        }

        // use ASCII codes to change heading relative to north:
        // N = 78, E = 69, W = 87, S = 83
        private static char TurnLeft(char currentHeading)
        {
            char newHeading = '\0';

            switch (currentHeading)
            {
                case 'N':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading + 9));
                    break;

                case 'E':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading + 9));
                    break;

                case 'W':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading - 4));
                    break;

                case 'S':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading - 14));
                    break;

                default:
                    break;
            }

            return newHeading;
        }

        private static char TurnRight(char currentHeading)
        {
            char newHeading = '\0';

            switch (currentHeading)
            {
                case 'N':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading - 9));
                    break;

                case 'E':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading + 14));
                    break;

                case 'W':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading - 9));
                    break;

                case 'S':
                    newHeading = char.Parse(char.ConvertFromUtf32(currentHeading + 4));
                    break;

                default:
                    break;
            }

            return newHeading;
        }
    }
}