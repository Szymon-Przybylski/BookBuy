using AuctionService.Data;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDatabaseContext _context;

        public BidPlacedConsumer(AuctionDatabaseContext dbContext)
        {
            _context = dbContext;
        }
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("--> Consuming bid placed");

            var auction = await _context.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

            if (auction.CurrentHighestBid == null
                || context.Message.BidStatus.Contains("Accepted")
                && context.Message.BidAmount > auction.CurrentHighestBid)
            {
                auction.CurrentHighestBid = context.Message.BidAmount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
