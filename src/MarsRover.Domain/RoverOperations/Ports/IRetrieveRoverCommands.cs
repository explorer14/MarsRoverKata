using MarsRover.Domain.RoverOperations.ValueTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Domain.RoverOperations.Ports
{
    public interface IRetrieveRoverCommands
    {
        Task<IReadOnlyCollection<RoverCommandParameters>> GetAll();
    }
}