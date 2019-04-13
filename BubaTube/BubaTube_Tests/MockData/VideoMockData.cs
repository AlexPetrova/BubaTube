using BubaTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    Id = random.Next(),
                    Title = RandomString(5),
                    Description = RandomString(10),
                    Path = RandomString(10),
                    AuthorId = RandomString(10)
                };

                videos.Add(video);
            }

            return videos;
        }
    }
}
