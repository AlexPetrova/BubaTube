using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchQueries searchQueries;

        public SearchController(ISearchQueries searchService)
        {
            this.searchQueries = searchService;
        }

        [HttpPost]
        public async Task<string> Search(string data)
        {
            var result = await this.searchQueries.GetJSON(data);
            return result;
        }

        public IActionResult SearchResultsBox()
        {
            return PartialView("_SearchResultsBox");
        }
    }
}
