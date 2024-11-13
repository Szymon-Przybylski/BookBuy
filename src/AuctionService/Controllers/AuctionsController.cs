using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDatabaseContext _context;
        private readonly IMapper _mapper;

        public AuctionsController(AuctionDatabaseContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
        {
            var auctions = await _context.Auctions
                .Include(x => x.Item)
                .OrderBy(x => x.Item.Name)
                .ToListAsync();
            return _mapper.Map<List<AuctionDto>>(auctions);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById (Guid id)
        {
            var auction = await _context.Auctions.
                Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(auction == null) return NotFound();

            return _mapper.Map<AuctionDto>(auction);
        }

        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);

            //todo add current user as seller
            auction.Seller = "Test";

            _context.Auctions.Add(auction);

            bool result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Could not save changes to the database");
            return CreatedAtAction(
                nameof(GetAuctionById),
                new {auction.Id},
                _mapper.Map<AuctionDto>(auction)
                );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
            var auction = await _context.Auctions.Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(auction == null) return NotFound();

            // check seller  identity

            auction.Item.Author = updateAuctionDto.Author ?? auction.Item.Author;
            auction.Item.Name = updateAuctionDto.Name ?? auction.Item.Name;
            auction.Item.Year = (int)(updateAuctionDto.Year ?? updateAuctionDto.Year);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();

            return BadRequest("Auction Update failed");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null) return NotFound();

            // check seller identity

            _context.Auctions.Remove(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if (result == false) return BadRequest("Could not delete specified auction");

            return Ok();
        }
    }
}
