﻿using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionsController(IAuctionRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint) 
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
        {
            return await _repository.GetAuctionsAsync(date);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById (Guid id)
        {
            var auction = await _repository.GetAuctionByIdAsync(id);

            if(auction == null) return NotFound();

            return auction;
        }   

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);

            auction.Seller = User.Identity.Name;

            _repository.AddAuction(auction);

            var newAuction = _mapper.Map<AuctionDto>(auction);

            await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

            bool result = await _repository.SaveChangesAsync();

            if (!result) return BadRequest("Could not save changes to the database");

            return CreatedAtAction(nameof(GetAuctionById), new {auction.Id}, newAuction);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
            var auction = await _repository.GetAuctionEntityById(id);
            if(auction == null) return NotFound();

            if(auction.Seller != User.Identity.Name) return Forbid();

            auction.Item.Author = updateAuctionDto.Author ?? auction.Item.Author;
            auction.Item.Name = updateAuctionDto.Name ?? auction.Item.Name;
            auction.Item.Year = (int)(updateAuctionDto.Year ?? auction.Item.Year);

            await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

            var result = await _repository.SaveChangesAsync();

            if (result) return Ok();

            return BadRequest("Auction Update failed");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _repository.GetAuctionEntityById(id);

            if (auction == null) return NotFound();

            if (auction.Seller != User.Identity.Name) return Forbid();

            _repository.RemoveAuction(auction);

            await _publishEndpoint.Publish<AuctionDeleted>(new AuctionDeleted { Id = auction.Id.ToString() });

            var result = await _repository.SaveChangesAsync();

            if (result == false) return BadRequest("Could not delete specified auction");

            return Ok();
        }
    }
}
