
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<AuctionDatabaseContext>());
        }

        private static void SeedData(AuctionDatabaseContext context)
        {
            context.Database.Migrate();

            if (context.Auctions.Any())
            {
                Console.WriteLine("Data already present  - no need to seed");
                return;
            }

            List<Auction> auctions = new List<Auction>()
            {
                // Book 1
            new Auction
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                ReservePrice = 20,
                Seller = "bob",
                AuctionEndingAt = DateTime.UtcNow.AddDays(10),
                Item = new Item
                {
                    Name = "Name",
                    Author = "Author",
                    Year = 2020,
                    ImageUrl = "https://cdn.prod.website-files.com/65469043b68c5cc835bcbbc5/6579b68d8a290d78c8b89d22_cover_splash_sm.jpg"
                }
            },
            // Book 2
            new Auction
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = Status.Live,
                ReservePrice = 90,
                Seller = "alice",
                AuctionEndingAt = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Name = "Name",
                    Author = "Author",
                    Year = 2018,
                    ImageUrl = "https://cdn.prod.website-files.com/65469043b68c5cc835bcbbc5/6579b68d8a290d78c8b89d22_cover_splash_sm.jpg"
                }
            },
            // 3 Book 3
            new Auction
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEndingAt = DateTime.UtcNow.AddDays(4),
                Item = new Item
                {
                    Name = "Name",
                    Author = "Author",
                    Year = 2023,
                    ImageUrl = "https://cdn.prod.website-files.com/65469043b68c5cc835bcbbc5/6579b68d8a290d78c8b89d22_cover_splash_sm.jpg"
                }
            },
            // Book 4
            new Auction
            {
                Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                Status = Status.FinishedReservedPriceNotMet,
                ReservePrice = 50,
                Seller = "tom",
                AuctionEndingAt = DateTime.UtcNow.AddDays(-10),
                Item = new Item
                {
                    Name = "Name",
                    Author = "Author",
                    Year = 2020,
                    ImageUrl = "https://cdn.prod.website-files.com/65469043b68c5cc835bcbbc5/6579b68d8a290d78c8b89d22_cover_splash_sm.jpg"
                }
            },
            };
            context.AddRange(auctions);
            context.SaveChanges();
        }
    }
}
