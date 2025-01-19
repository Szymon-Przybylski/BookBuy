using MongoDB.Entities;

namespace BiddingService.Models;

public class Auction : Entity
{
    public DateTime AuctionEndingAt {get; set;}
    public string Seller {get; set;}
    public int ReservePrice {get; set;}
    public bool IsFinished { get; set;}
}
