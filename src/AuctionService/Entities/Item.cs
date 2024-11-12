using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required int Year { get; set; }
        public required string ImageUrl { get; set; }

        //nav properties
        public Auction? Auction { get; set; }
        public Guid? AuctionId { get; set; }
    }
}
