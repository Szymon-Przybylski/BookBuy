using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly AuctionDatabaseContext _context;

        public AuctionFinishedConsumer(AuctionDatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            Console.WriteLine("--> Consuming auction finished");

            var auction = await _context.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.SoldAmount;
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
                ? Status.Finished : Status.FinishedReservedPriceNotMet;

            await _context.SaveChangesAsync();
        }
    }
}
