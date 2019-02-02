using BubaTube.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class SearchController : Controller
    {
        private ISearchService searchService;

        public SearchController(ISearchService searchService)
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
