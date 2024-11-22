using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

        //nav properties
        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}
