using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs
{
    public class UpdateAuctionDto
    {
        // Item data
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Author { get; set; }
        public int? Year { get; set; }
    }
}
