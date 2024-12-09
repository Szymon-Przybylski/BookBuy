using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Utilities;
using Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests
{
    [Collection("Shared collection")]
    public class AuctionServiceBusTests : IAsyncLifetime
    {
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;
        private readonly ITestHarness _testHarness;

        public AuctionServiceBusTests(CustomWebAppFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _testHarness = _factory.Services.GetTestHarness();
        }

        public Task DisposeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDatabaseContext>();
            DatabaseHelper.ReinitializeDatabase(db);
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        private static CreateAuctionDto GetAuctionForCreate()
        {
            return new CreateAuctionDto
            {
                Name = "test",
                Author = "test",
                Year = 1234,
                ImageUrl = "test",
                ReservePrice = 100,
                AuctionEndingAt = DateTime.UtcNow,

            };
        }

        [Fact]
        public async Task CreateAuctionWithValidObjectShouldPublishAuctionCreatedEvent()
        {
            var auction = GetAuctionForCreate();
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetJwtBearerForUser("bob"));

            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            response.EnsureSuccessStatusCode();
            Assert.True(await _testHarness.Published.Any<AuctionCreated>());
        }
    }
}
