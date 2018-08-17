﻿namespace BubaTube.Data.Models
{
    public class VideoCategory
    {
        public int VideoId { get; set; }

        public Video Video { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
