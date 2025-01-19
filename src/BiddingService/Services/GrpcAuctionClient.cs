using AuctionService;
using BiddingService.Models;
using Grpc.Net.Client;

namespace BiddingService.Services;

public class GrpcAuctionClient
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public GrpcAuctionClient(ILogger<GrpcAuctionClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public Auction GetAuction(string id) {

        _logger.LogInformation("Calling gRPC Service");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcAuction"]);
        var client = new GrpcAuction.GrpcAuctionClient(channel);
        var request = new GetAuctionRequest{
            Id = id
        };

        try
        {
            var reply = client.GetAuction(request);
            var auction = new Auction {
                ID = reply.Auction.Id,
                AuctionEndingAt = DateTime.Parse(reply.Auction.AuctionEndingAt),
                Seller = reply.Auction.Seller,
                ReservePrice = reply.Auction.ReservePrice
            };

            return auction;
        }
        catch (Exception Exception)
        {
            _logger.LogError(Exception, "Could not call gRPC server");
            return null;
        }
    }
}
