using MarsRover.Domain.RoverOperations.ValueTypes;
using System;

namespace MarsRover.Domain.RoverOperations.Entities
{
    public sealed class Rover
    {
        private readonly Terrain terrain;

        /// <summary>
        /// Rover needs to know about the terrain that its got to navigate so
        /// it can do so safely.
        /// </summary>
        /// <param name="terrain"></param>
        /// <param name="startingPositionX">starting x-coordinate of the rover</param>
        /// <param name="startingPositionY">starting y-coordinate of the rover</param>
        /// <param name="startingHeading">the direction rover is facing to start with</param>
        public Rover(
            Terrain terrain,
            int startingPositionX,
            int startingPositionY,
            Direction startingHeading)
        {
            EnsureRoverIsWithinTerrain(
                terrain, startingPositionX, startingPositionY);
            EnsureStartHeadingIsValid(startingHeading);

            this.terrain = terrain;
            StartingPositionX = startingPositionX;
            StartingPositionY = startingPositionY;

            NewPositionX = StartingPositionX;
            NewPositionY = StartingPositionY;

            StartingHeading = startingHeading;
            NewHeading = StartingHeading;

            Id = Guid.NewGuid();
        }

        private static void EnsureStartHeadingIsValid(Direction startingHeading)
        {
            if (!Enum.IsDefined(typeof(Direction), startingHeading))
                throw new ArgumentOutOfRangeException(
                    nameof(startingHeading),
                    "Starting heading is invalid, must be one of NORTH, EAST, WEST or SOUTH");
        }

        private static void EnsureRoverIsWithinTerrain(Terrain terrain, int startingPositionX, int startingPositionY)
        {
            if (terrain == null)
                throw new ArgumentNullException(
                    nameof(terrain),
                    "Rover cannot navigate on a non-existent terrain!");

            if (IsStartingPositionOutsideTerrain(
                    terrain,
                    startingPositionX,
                    startingPositionY))
                throw new ArgumentOutOfRangeException(
                    $"{nameof(startingPositionX)},{nameof(startingPositionY)}",
                    "Rover cannot start outside the terrain");
        }

        public CurrentRoverPosition GetCurrentPosition() =>
            new CurrentRoverPosition(
                Id,
                NewPositionX,
                NewPositionY,
                NewHeading);

        public int StartingPositionX { get; }
        public int StartingPositionY { get; }
        public Direction StartingHeading { get; }
        public Guid Id { get; }

        internal Direction NewHeading { get; private set; }
        internal int NewPositionX { get; private set; }
        internal int NewPositionY { get; private set; }

        /// <summary>
        /// Assumptions:
        /// a. The rover will only move by 1 unit up or down based on its current heading
        /// b. No diagonal movements are allowed or supported for e.g. NE, SW etc
        /// </summary>
        public CurrentRoverPosition Move()
        {
            TryMoveNorth();
            TryMoveEast();
            TryMoveWest();
            TryMoveSouth();

            return new CurrentRoverPosition(
                Id,
                NewPositionX,
                NewPositionY,
                NewHeading);
        }

        private void TryMoveSouth()
        {
            if (NewHeading == Direction.South)
            {
                if (NewPositionY - 1 < terrain.MinY)
                    throw new InvalidOperationException(
                        $"Rover cannot go off terrain @ {NewPositionX},{NewPositionY}");

                NewPositionY--;
            }
        }

        private void TryMoveWest()
        {
            if (NewHeading == Direction.West)
            {
                if (NewPositionX - 1 < terrain.MinX)
                    throw new InvalidOperationException(
                        $"Rover cannot go off terrain @ {NewPositionX},{NewPositionY}");

                NewPositionX--;
            }
        }

        private void TryMoveEast()
        {
            if (NewHeading == Direction.East)
            {
                if (NewPositionX + 1 > terrain.MaxX)
                    throw new InvalidOperationException(
                        $"Rover cannot go off terrain @ {NewPositionX},{NewPositionY}");

                NewPositionX++;
            }
        }

        private void TryMoveNorth()
        {
            if (NewHeading == Direction.North)
            {
                if (NewPositionY + 1 > terrain.MaxY)
                    throw new InvalidOperationException(
                        $"Rover cannot go off terrain @ {NewPositionX},{NewPositionY}");

                NewPositionY++;
            }
        }

        /// <summary>
        /// Assumptions:
        /// a. Rover turns i.e. pivots on its axis without altering its current position
        /// </summary>
        public CurrentRoverPosition TurnLeft()
        {
            switch (NewHeading)
            {
                case Direction.North:
                    NewHeading = NewHeading + 9;
                    break;

                case Direction.East:
                    NewHeading = NewHeading + 9;
                    break;

                case Direction.West:
                    NewHeading = NewHeading - 4;
                    break;

                case Direction.South:
                    NewHeading = NewHeading - 14;
                    break;

                default:
                    break;
            }

            return new CurrentRoverPosition(
                Id,
                NewPositionX,
                NewPositionY,
                NewHeading);
        }

        /// <summary>
        /// Assumptions:
        /// a. Rover turns i.e. pivots on its axis without altering its current position
        /// </summary>
        public CurrentRoverPosition TurnRight()
        {
            switch (NewHeading)
            {
                case Direction.North:
                    NewHeading = NewHeading - 9;
                    break;

                case Direction.East:
                    NewHeading = NewHeading + 14;
                    break;

                case Direction.West:
                    NewHeading = NewHeading - 9;
                    break;

                case Direction.South:
                    NewHeading = NewHeading + 4;
                    break;

                default:
                    break;
            }

            return new CurrentRoverPosition(
                Id,
                NewPositionX,
                NewPositionY,
                NewHeading);
        }

        private static bool IsStartingPositionOutsideTerrain(
            Terrain terrain,
            int startingPositionX,
            int startingPositionY)
        {
            return startingPositionX > terrain.MaxX ||
                   startingPositionX < terrain.MinX ||
                   startingPositionY > terrain.MaxY ||
                   startingPositionY < terrain.MinY;
        }
    }
}