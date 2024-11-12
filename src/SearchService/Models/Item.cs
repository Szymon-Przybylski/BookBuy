using MongoDB.Entities;

namespace SearchService.Models
{
    public class Item : Entity
    {
        // Auction data
        public int ReservePrice { get; set; }
        public required string Seller { get; set; }
        public string? Winner { get; set; }
        public int SoldAmount { get; set; }
        public int CurrentHighestBid { get; set; }
        public int MyProperty { get; set; }
        public DateTime AuctionCreatedAt { get; set; }
        public DateTime AuctionUpdatedAt { get; set; }
        public DateTime AuctionEndingAt { get; set; }
        public required string Status { get; set; }

        // Item data
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required int Year { get; set; }
        public required string ImageUrl { get; set; }
    }
}
