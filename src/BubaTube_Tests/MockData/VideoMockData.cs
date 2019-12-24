using BubaTube.Data.Models;
using BubaTube_Tests.Helpers;
using System.Collections.Generic;

namespace BubaTube_Tests.MockData
{
    public static class VideoMockData
    {
        public static IEnumerable<Video> GetVideos(int count)
        {
            var videos = new List<Video>();

            for (int i = 0; i < count; i++)
            {
                var video = new Video()
                {
                    Id = Random.Number(),
                    Title = Random.String(5),
                    Description = Random.String(10),
                    Path = Random.String(10),
                    AuthorId = Random.String(10)
                };

                videos.Add(video);
            }

            return videos;
        }
    }
}
