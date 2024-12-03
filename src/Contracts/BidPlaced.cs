using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class BidPlaced
    {
        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string Bidder { get; set; }
        public DateTime BidDate { get; set; }
        public int BidAmount { get; set; }
        public string BidStatus { get; set; }
    }
}
