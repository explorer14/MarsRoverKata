using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.ValueTypes;
using System;
using System.Threading.Tasks;

namespace MarsRover.ConsoleApp
{
    internal class ConsoleTransmitter : ITransmitRoverPosition
    {
        public Task Transmit(CurrentRoverPosition currentRoverPosition)
        {
            // Usually this will be some sort of DTO to decouple
            // domain value type from its outgoing representation
            Console.WriteLine(currentRoverPosition.ToString());

            return Task.CompletedTask;
        }
    }
}