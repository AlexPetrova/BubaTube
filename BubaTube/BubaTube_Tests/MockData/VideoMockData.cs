using BubaTube.Data.Models;
using System;
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
                    Id = Helpers.Random.Number(),
                    Title = Helpers.Random.String(5),
                    Description = Helpers.Random.String(10),
                    Path = Helpers.Random.String(10),
                    AuthorId = Helpers.Random.String(10)
                };

                videos.Add(video);
            }

            return videos;
        }
    }
}
