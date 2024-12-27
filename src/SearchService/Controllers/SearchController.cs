using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery]SearchItemParameters parameters)
        {
            var query = DB.PagedSearch<Item, Item>();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query.Match(Search.Full, parameters.SearchTerm).SortByTextScore();
            }

            query = parameters.OrderBy switch
            {
                "name" => query.Sort(x => x.Ascending(a => a.Name)),
                "new" => query.Sort(x => x.Descending(a => a.AuctionCreatedAt)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEndingAt))
            };

            query = parameters.FilterBy switch
            {
                "finished" => query.Match(x => x.AuctionEndingAt < DateTime.UtcNow),
                "endingSoon" => query.Match
                    (x => x.AuctionEndingAt < DateTime.UtcNow.AddHours(6) && x.AuctionEndingAt > DateTime.UtcNow),
                _ => query.Match(x => x.AuctionEndingAt >= DateTime.UtcNow)
            };

            if (!string.IsNullOrEmpty(parameters.Seller))
            {
                query.Match(x => x.Seller == parameters.Seller);
            }

            if (!string.IsNullOrEmpty(parameters.Winner))
            {
                query.Match(x => x.Winner == parameters.Winner);
            }

            query.PageNumber(parameters.PageNumber);

            query.PageSize(parameters.PageSize);

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                result = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}
