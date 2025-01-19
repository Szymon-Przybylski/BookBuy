using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController]
[Route("api/bids")]
public class BidsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BidsController(IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BidDto>> PlaceBid(string auctionId, int bidAmount) {

        var auction = await DB.Find<Auction>().OneAsync(auctionId);

        if (auction == null) {
            return NotFound();
        }

        if(auction.Seller == User.Identity.Name) {
            return BadRequest("You cannot bid on your own auction");
        }

        var bid = new Bid {
            BidAmount = bidAmount,
            AuctionId = auctionId,
            Bidder = User.Identity.Name
        };

        if(auction.AuctionEndingAt < DateTime.UtcNow) {
            bid.BidStatus = BidStatus.Finished;
        } else {

            var currentHighestBid = await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(x => x.BidAmount))
            .ExecuteFirstAsync();

            if(currentHighestBid != null && bidAmount > currentHighestBid.BidAmount || currentHighestBid == null) {
            bid.BidStatus = bidAmount > auction.ReservePrice
                ? BidStatus.Accepted
                : BidStatus.AcceptedBelowReservePrice;
            }

            if(currentHighestBid != null && bidAmount <= currentHighestBid.BidAmount) {
            bid.BidStatus = BidStatus.BidTooLow;
            }
        }

        await DB.SaveAsync(bid);

        await _publishEndpoint.Publish(_mapper.Map<BidPlaced>(bid));

        var BidDto = _mapper.Map<BidDto>(bid);

        return Ok(BidDto);
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<List<BidDto>>> GetBidsForAuction(string auctionId) {

        var bids = await DB.Find<Bid>()
            .Match(a => a.AuctionId == auctionId)
            .Sort(b => b.Descending(a => a.BidDate))
            .ExecuteAsync();

        var bidsDto =  bids.Select(_mapper.Map<BidDto>).ToList();

        return bidsDto;
    }
}
