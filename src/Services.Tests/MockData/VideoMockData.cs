using Contracts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube_Tests.MockData
{
    public static class VideoMockData
    {
        private static readonly Random random = new Random();

        public static List<Video> GetApprovedVideos(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => new Video()
                {
                    Id = GetRandomNumber(),
                    Title = GetRandomString(5),
                    Description = GetRandomString(10),
                    FileName = GetRandomString(10),
                    AuthorId = GetRandomString(10),
                    IsApproved = true
                })
                .ToList();
        }

        public static List<Video> GetDeletedVideos(int count)
        {
            return Enumerable.Range(0, count)
                   .Select(_ => new Video()
                   {
                       Id = GetRandomNumber(),
                       Title = GetRandomString(5),
                       Description = GetRandomString(10),
                       FileName = GetRandomString(10),
                       AuthorId = GetRandomString(10),
                       IsDeleted = true
                   })
                   .ToList();
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        public static int GetRandomNumber()
        {
            return random.Next();
        }
    }
}
