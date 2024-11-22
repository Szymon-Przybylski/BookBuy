using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class AuctionUpdated
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public int Year { get; set; }
    }
}
