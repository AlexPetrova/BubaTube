using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchQueries searchService;

        public SearchController(ISearchQueries searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<string> Search(string data)
        {
            var result = await this.searchService.GetSearchResultsJSON(data);
            return result;
        }

        public IActionResult SearchResultsBox()
        {
            return PartialView("_SearchResultsBox");
        }
    }
}
