using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
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

            if (count == 0) {
                Console.WriteLine("No data found - will attempt to seed");
                var itemData = await File.ReadAllTextAsync("Data/Auctions.json");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var items = JsonSerializer  .Deserialize<List<Item>>(itemData, options);
                await DB.SaveAsync(items);
            }
        }
    }
}
