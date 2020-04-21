using BubaTube_Tests.MockData;
using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Services.Tests.Get
{
    public class VideoQueriesQueries_Should
    {
        static readonly Expression<Func<Video, VideoDTO>> fakeDTOMapper = v => new VideoDTO();
        static readonly Expression<Func<Video, VideoPreviewDTO>> fakePreviewDTOMapper = v => new VideoPreviewDTO();

        [Fact]
        public void ReturnsRecentVideos()
        {
            var options = DbContextMock.GetOptions("ReturnsRecentVideos");

            using (var actContext = new BubaTubeDbContext(options))
            {
                actContext.Videos.AddRange(VideoMockData.GetApprovedVideos(10));
                actContext.Videos.AddRange(VideoMockData.GetDeletedVideos(10));

                actContext.SaveChanges();
                
                var videoQueries = new VideoQueries(actContext, fakeDTOMapper, fakePreviewDTOMapper);

                var resentVideos = videoQueries.MostResentVideos();

                Assert.Equal(10, resentVideos.Count);
            }
        }
    }
}
