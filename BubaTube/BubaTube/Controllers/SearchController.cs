using BubaTube.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BubaTube.Controllers
{
    public class SearchController : Controller
    {
        private ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IActionResult Search(string input)
        {
            //json
            return this.Ok();
        }
    }
}
