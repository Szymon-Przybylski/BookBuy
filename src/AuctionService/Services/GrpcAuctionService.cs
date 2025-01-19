using AuctionService.Data;
using Grpc.Core;

namespace AuctionService.Services;

public class GrpcAuctionService : GrpcAuction.GrpcAuctionBase
{
    private readonly AuctionDatabaseContext _databaseContext;

    public GrpcAuctionService(AuctionDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context) {

        Console.WriteLine("===> Received gRPC request for auction");

        var auction = await _databaseContext.Auctions.FindAsync(Guid.Parse(request.Id));

        if(auction == null) throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

        var response = new GrpcAuctionResponse {

            Auction = new GrpcAuctionModel {
                AuctionEndingAt = auction.AuctionEndingAt.ToString(),
                Id = auction.Id.ToString(),
                ReservePrice = auction.ReservePrice,
                Seller = auction.Seller,
            }
        };

        return response;
    }
}
