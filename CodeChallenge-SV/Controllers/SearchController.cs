using CodeChallenge_SV.BusinessLogicLayer;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge_SV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly SearchBll _searchBll;

        public SearchController(ILogger<SearchController> logger, ISearchDal searchDal)
        {
            _logger = logger;
            _searchBll = new SearchBll(searchDal);
        }

        [HttpGet("{searchInput}")]
        public async Task<List<SearchResult>> Search(string searchInput)
        {
            return await _searchBll.Search(searchInput);
        }
    }
}
