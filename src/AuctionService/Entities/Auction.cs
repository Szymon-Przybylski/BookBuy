namespace AuctionService.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0;
        public required string Seller { get; set; }
        public string? Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighestBid { get; set; }
        public int? MyProperty { get; set; }
        public DateTime AuctionCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionUpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEndingAt {  get; set; }
        public Status Status { get; set; }
        public required Item Item { get; set; }


    }
}
