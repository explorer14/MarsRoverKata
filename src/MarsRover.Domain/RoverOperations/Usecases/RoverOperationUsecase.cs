using MarsRover.Domain.RoverOperations.Factories;
using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Domain.RoverOperations.Usecases
{
    public sealed class RoverOperationUsecase
    {
        private readonly IRetrieveRoverCommands roverCommandRetriever;
        private readonly ITransmitRoverPosition roverPositionTransmitter;

        public RoverOperationUsecase(
            IRetrieveRoverCommands roverCommandRetriever,
            ITransmitRoverPosition roverPositionTransmitter)
        {
            this.roverCommandRetriever = roverCommandRetriever;
            this.roverPositionTransmitter = roverPositionTransmitter;
        }

        public async Task StartRoverOperation()
        {
            var commandsToExecute = await roverCommandRetriever.GetAll();
            EnsureNotEmpty(commandsToExecute);

            // Assumption: we are going to operate rovers sequentially
            // i.e. the first rover will execute all its commands, then the next and so on...
            foreach (var roverCommand in commandsToExecute)
            {
                if (roverCommand.ManouvreCommands.Any())
                {
                    var rover = RoverFactory.CreateWith(roverCommand);

                    foreach (var manouvreCommand in roverCommand.ManouvreCommands)
                    {
                        // This is not necessarily the prettiest because if NASA asks me to extend
                        // the capability of the Rover, then I will have to change rover, use case,
                        // parser and add an extra RoverCommands member. This surface area could be
                        // reduced by moving the responsbility of instantiating the rover and
                        // mapping input to rover functions via delegates, into the parser itself
                        // since each parser will need to determine that for itself anyway. The use
                        // case will then simply loop over all the commands and execute them one by
                        // one without having to know which is which (i.e. no switch...case).
                        // Ofcourse, this only works as long as the commands have a homogenous
                        // signature and return types. For now though, this will do since we are not
                        // extending rover behaviour!
                        CurrentRoverPosition currentRoverPosition = default;

                        switch (manouvreCommand)
                        {
                            case RoverCommands.TurnLeft:
                                currentRoverPosition = rover.TurnLeft();
                                break;

                            case RoverCommands.TurnRight:
                                currentRoverPosition = rover.TurnRight();
                                break;

                            case RoverCommands.Move:
                                currentRoverPosition = rover.Move();
                                break;

                            default:
                                throw new InvalidOperationException(
                                    $"Rover command {manouvreCommand} is not supported!");
                        }

                        await roverPositionTransmitter.Transmit(
                            currentRoverPosition);
                    }
                }
            }
        }

        private static void EnsureNotEmpty(
            IReadOnlyCollection<RoverCommandParameters> commandsToExecute)
        {
            if (!commandsToExecute.Any())
                throw new ArgumentOutOfRangeException(
                    nameof(commandsToExecute),
                    "No commands found to execute! Please try again!");
        }
    }
}