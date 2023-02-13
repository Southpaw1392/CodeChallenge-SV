using CodeChallenge_SV.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge_SV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly SearchBll _searchBll;

        public SearchController(ILogger<SearchController> logger, SearchBll searchBll)
        {
            _logger = logger;
            _searchBll = searchBll;
        }
    }
}
