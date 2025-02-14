namespace BiddingService.DTOs;

public class BidDto
{
    public string Id {get; set;}
    public string AuctionId {get; set;}
    public string Bidder {get; set;}
    public DateTime BidDate{get; set;}
    public int BidAmount { get; set;}
    public string BidStatus {get; set;}
}
