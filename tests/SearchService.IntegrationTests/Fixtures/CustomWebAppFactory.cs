using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Mongo2Go;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.IntegrationTests.Fixtures
{
    public class CustomWebAppFactory : WebApplicationFactory<Program>
    {
        private readonly MongoDbRunner _runner;

        public CustomWebAppFactory()
        {
            _runner = MongoDbRunner.Start();
            DB.InitAsync("test-db", MongoClientSettings.FromConnectionString(_runner.ConnectionString));

            DB.Index<Item>()
                .Key(x => x.Name, KeyType.Text)
                .Key(x => x.Author, KeyType.Text)
                .Key(x => x.Year, KeyType.Text)
                .CreateAsync();

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureTestServices(services =>
            {
                services.AddMassTransitTestHarness();
            });
        }

        protected override void Dispose(bool disposing)
        {
            _runner.Dispose();
        }
    }
}
