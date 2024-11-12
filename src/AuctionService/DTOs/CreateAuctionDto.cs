using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs
{
    public class CreateAuctionDto
    {
        // Auction data
        [Required]
        public int ReservePrice { get; set; }
        [Required]
        public DateTime AuctionEndingAt { get; set; }
        // Item data
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Author { get; set; }
        [Required]
        public required int Year { get; set; }
        [Required]
        public required string ImageUrl { get; set; }
    }
}
