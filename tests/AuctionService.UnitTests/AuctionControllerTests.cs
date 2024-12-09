using AuctionService.Controllers;
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.RequestHelpers;
using AuctionService.UnitTests.Utilities;
using AutoFixture;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.UnitTests
{
    public class AuctionControllerTests
    {

        private readonly Mock<IAuctionRepository> _repositoryMock;
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly Fixture _fixture;
        private readonly AuctionsController _controller;
        private readonly IMapper _mapper;
        public AuctionControllerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IAuctionRepository>();
            _publishEndpointMock = new Mock<IPublishEndpoint>();

            var mockMapper = new MapperConfiguration(mc =>
            {
                mc.AddMaps(typeof(MappingProfiles).Assembly);
            }).CreateMapper().ConfigurationProvider;

            _mapper = new Mapper(mockMapper);

            _controller = new AuctionsController(_repositoryMock.Object, _mapper, _publishEndpointMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = HelperMethods.GetClaimsPrincipal()
                    }
                },
            };
        }

        [Fact]
        public async Task GetAuctionsWithNoParametersShouldReturnFourAuctions()
        {
            var auctions = _fixture.CreateMany<AuctionDto>(4).ToList();
            _repositoryMock.Setup(repo => repo.GetAuctionsAsync(null)).ReturnsAsync(auctions);

            var result = await _controller.GetAllAuctions(null);

            Assert.Equal(4, result.Value.Count);
            Assert.IsType<ActionResult<List<AuctionDto>>>(result);

        }

        [Fact]
        public async Task GetAuctionByIdWithValidGuidShouldReturnAuction()
        {
            var auction = _fixture.Create<AuctionDto>();
            _repositoryMock.Setup(repo => repo.GetAuctionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(auction);

            var result = await _controller.GetAuctionById(auction.Id);

            Assert.Equal(auction.Name, result.Value.Name);
            Assert.IsType<ActionResult<AuctionDto>>(result);

        }

        [Fact]
        public async Task GetAuctionByIdWithInvalidGuidShouldReturnNotFound()
        {
            _repositoryMock.Setup(repo => repo.GetAuctionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var result = await _controller.GetAuctionById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task CreateAuctionWithValidCreateAuctionDtoReturnsCreatedAtActionResult()
        {
            var auction = _fixture.Create<CreateAuctionDto>();

            _repositoryMock.Setup(repo => repo.AddAuction(It.IsAny<Auction>()));
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var result = await _controller.CreateAuction(auction);
            var createdResult = result.Result as CreatedAtActionResult;

            Assert.NotNull(createdResult);
            Assert.Equal("GetAuctionById", createdResult.ActionName);
            Assert.IsType<AuctionDto>(createdResult.Value);
        }

        [Fact]
        public async Task CreateAuctionWithFailedSaveShouldReturnBadRequest()
        {
            var auction = _fixture.Create<CreateAuctionDto>();

            _repositoryMock.Setup(repo => repo.AddAuction(It.IsAny<Auction>()));
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(false);

            var result = await _controller.CreateAuction(auction);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public async Task UpdateAuctionWithUpdateAuctionDtoShouldReturnOk()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
            auction.Item = _fixture.Build<Item>().Without(x => x.Auction).Create();
            auction.Seller = "test";

            var updateDto = _fixture.Create<UpdateAuctionDto>();
            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(auction);
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var result = await _controller.UpdateAuction(auction.Id, updateDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateAuctionWithInvalidUserShouldReturnForbid()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
            auction.Seller = "notTest";

            var updateDto = _fixture.Create<UpdateAuctionDto>();
            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(auction);

            var result = await _controller.UpdateAuction(auction.Id, updateDto);

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task UpdateAuctionWithInvalidGuidShouldReturnNotFound()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();

            var updateDto = _fixture.Create<UpdateAuctionDto>();
            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var result = await _controller.UpdateAuction(auction.Id, updateDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAuctionWithValidUserShouldReturnOk()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
            auction.Seller = "test";

            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(auction);
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var result = await _controller.DeleteAuction(auction.Id);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteAuctionWithInvalidGuidShouldReturnNotFound()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();

            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var result = await _controller.DeleteAuction(auction.Id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAuctionWithInvalidUserShouldReturnForbid()
        {
            var auction = _fixture.Build<Auction>().Without(x => x.Item).Create();
            auction.Seller = "notTest";

            _repositoryMock.Setup(repo => repo.GetAuctionEntityById(It.IsAny<Guid>())).ReturnsAsync(auction);

            var result = await _controller.DeleteAuction(auction.Id);

            Assert.IsType<ForbidResult>(result);
        }
    }
}
