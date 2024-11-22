using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class AuctionCreated
    {
        // Auction data
        public Guid Id { get; set; }
        public int ReservePrice { get; set; }
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int SoldAmount { get; set; }
        public int CurrentHighestBid { get; set; }
        public int MyProperty { get; set; }
        public DateTime AuctionCreatedAt { get; set; }
        public DateTime AuctionUpdatedAt { get; set; }
        public DateTime AuctionEndingAt { get; set; }
        public string Status { get; set; }

        // Item data
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
    }
}
