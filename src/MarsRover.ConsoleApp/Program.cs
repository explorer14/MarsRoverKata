using MarsRover.Domain.RoverOperations.Ports;
using MarsRover.Domain.RoverOperations.Usecases;
using System;

namespace MarsRover.ConsoleApp
{
    public class Program
    {
        public static ITransmitRoverPosition TransmitterToUse { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                var roverOpertionUsecase = new RoverOperationUsecase(
                    new CommandLineInputParser(args),
                    TransmitterToUse);

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