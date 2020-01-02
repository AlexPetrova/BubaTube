using BubaTube_Tests.Helpers;
using Contracts.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube_Tests.MockData
{
    public static class VideoMockData
    {
        public static IEnumerable<Video> GetVideos(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => new Video()
                    {
                        Id = Random.Number(),
                        Title = Random.String(5),
                        Description = Random.String(10),
                        Path = Random.String(10),
                        AuthorId = Random.String(10)
                    })
                .ToList();
        }
    }
}
