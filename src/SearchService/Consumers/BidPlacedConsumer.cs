using Contracts;
using MassTransit;
using MongoDB.Entities;
using Polly;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("--> Consuming bid placed");

            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

            if (context.Message.BidStatus.Contains("Accepted")
                && context.Message.BidAmount > auction.CurrentHighestBid)
            {
                auction.CurrentHighestBid = context.Message.BidAmount;
                await auction.SaveAsync();
            }
        }
    }
}
