using MarsRover.Domain.RoverOperations.ValueTypes;
using System.Threading.Tasks;

namespace MarsRover.Domain.RoverOperations.Ports
{
    public interface ITransmitRoverPosition
    {
        Task Transmit(CurrentRoverPosition currentRoverPosition);
    }
}