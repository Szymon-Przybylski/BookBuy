using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;
using System.Text.Json;

namespace SearchService.Data
{
    public class DbInitializer
    {

        public static async Task InitializeDatabase(WebApplication app)
        {
            await DB.InitAsync("SearchServiceDatabase",
                MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                .Key(x => x.Name, KeyType.Text)
                .Key(x => x.Author, KeyType.Text)
                .Key(x => x.Year, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();

            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();

            var items = await httpClient.GetItemsForSearchServiceDatabase();

            Console.WriteLine(items.Count + " returned from auction service");

            if (items.Count > 0)
            {
                await DB.SaveAsync(items);
            }

        }
    }
}
