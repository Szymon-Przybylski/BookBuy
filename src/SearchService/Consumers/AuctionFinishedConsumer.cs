using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = (int)context.Message.SoldAmount;
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
                ? Status.Finished.ToString() : Status.FinishedReservedPriceNotMet.ToString();

            await auction.SaveAsync();
        }
    }
}
