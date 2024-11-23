using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
        {
            Console.WriteLine("--> consuming faulty creation");

            var exception = context.Message.Exceptions.First();

            if (exception.ExceptionType == "System.ArgumentException") {
                context.Message.Message.Name = "SupremeBook";
                await context.Publish(context.Message.Message);
            } else
            {
                Console.WriteLine("Not an argument exception");
            }
        }
    }
}
