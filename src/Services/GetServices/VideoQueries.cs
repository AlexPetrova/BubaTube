﻿using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services.GetServices
{
    public class VideoQueries : IVideoQueries
    {
        private readonly BubaTubeDbContext context;
        private readonly Func<Video, VideoDTO> videoMapper;
        private const int DefaultCountForResentVideos = 20;

        public VideoQueries(
            BubaTubeDbContext context,
            Func<Video, VideoDTO> videoMapper)
        {
            this.context = context;
            this.videoMapper = videoMapper;
        }

        public IReadOnlyCollection<VideoDTO> MostResentVideos()
        {
            var resentVideos = this.context.Videos
                .Where(x => x.IsАpproved == true)
                .TakeLast(DefaultCountForResentVideos)
                .Select(videoMapper)
                .ToList();

            return resentVideos;
        }

        public IReadOnlyCollection<VideoDTO> PopularVideos()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<VideoDTO> UserMayLikeVideos(User user)
        {
            throw new NotImplementedException();
        }
    }
}