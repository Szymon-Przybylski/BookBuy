using BiddingService.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Consumers;

public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        var auction = new Auction {
            ID = context.Message.Id.ToString(),
            Seller = context.Message.Seller,
            AuctionEndingAt = context.Message.AuctionEndingAt,
            ReservePrice = context.Message.ReservePrice
        };

        await auction.SaveAsync();
    }
}
