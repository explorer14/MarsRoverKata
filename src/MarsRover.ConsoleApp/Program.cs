using MarsRover.Domain.RoverOperations.Usecases;
using System;

namespace MarsRover.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var roverOpertionUsecase = new RoverOperationUsecase(
                    new CommandLineInputParser(args),
                    new ConsoleTransmitter());

                roverOpertionUsecase.StartRoverOperation().Wait();
            }
            catch (AggregateException aggex)
            {
                Console.WriteLine(aggex.Flatten().InnerException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}