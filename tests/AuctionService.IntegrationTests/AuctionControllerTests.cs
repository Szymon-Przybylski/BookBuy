using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Utilities;
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
    public class AuctionControllerTests : IAsyncLifetime
    {
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;
        private const string _auctionId = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

        public AuctionControllerTests(CustomWebAppFactory factory)
        {
            _factory = factory;
            _httpClient =  _factory.CreateClient();
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
        public async Task GetAuctionsShouldReturnAllAuctions()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("api/auctions");

            Assert.Equal(4, response.Count);
        }

        [Fact]
        public async Task GetAuctionByIdWithValidGuidShouldReturnAuction()
        {
            var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"api/auctions/{_auctionId}");

            Assert.Equal("Name", response.Name);
        }

        [Fact]
        public async Task GetAuctionByIdWithGuidNotInDatabaseShouldReturnNotFound()
        {
            var response = await _httpClient.GetAsync($"api/auctions/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAuctionByIdWithInvalidGuidShouldReturnBadRequest()
        {
            var response = await _httpClient.GetAsync("api/auctions/Guid");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateAuctionWithNoAuthenticationShouldReturnUnauthorized()
        {
            var auction = GetAuctionForCreate();

            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreateAuctionWithAuthenticationShouldReturnCreated()
        {
            var auction = GetAuctionForCreate();
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetJwtBearerForUser("bob"));

            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();

            Assert.Equal("bob", createdAuction.Seller);
        }

        [Fact]
        public async Task CreateAuctionWithInvalidAuctionDtoShouldReturnBadRequest()
        {
            var auction = GetAuctionForCreate();
            auction.Name = null;

            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetJwtBearerForUser("bob"));

            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAuctionWithValidUpdateDtoAndUserShouldReturnOk()
        {
            var updateAuction = new UpdateAuctionDto
            {
                Name = "updatedName",
                Author = "updatedAuthor",
                Year = 1999
            };
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetJwtBearerForUser("bob"));

            var response = await _httpClient.PutAsJsonAsync($"api/auctions/{_auctionId}", updateAuction);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAuctionWithValidUpdateDtoAndInvalidUserShouldReturnForbidden()
        {
            var updateAuction = new UpdateAuctionDto
            {
                Name = "updatedName",
                Author = "updatedAuthor",
                Year = 1999
            };
            _httpClient.SetFakeJwtBearerToken(AuthenticationHelper.GetJwtBearerForUser("notbob"));

            var response = await _httpClient.PutAsJsonAsync($"api/auctions/{_auctionId}", updateAuction);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
