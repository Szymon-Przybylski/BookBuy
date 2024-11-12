using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems(string? searchTerm, int? pageNumber = 1, int? pageSize = 4)
        {
            var query = DB.PagedSearch<Item>();
            query.Sort(x => x.Ascending(a => a.Name));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }

            if (pageNumber != null) {
                query.PageNumber((int)pageNumber);
            }
            if (pageSize != null)
            {
                query.PageSize((int)pageSize);
            }

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
