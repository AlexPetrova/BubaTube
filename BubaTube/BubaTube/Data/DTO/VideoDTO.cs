﻿using BubaTube.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.DTO
{
    public class VideoDTO
    {
        //maybe use it when showing search results 

        [Required]
        [StringLength(200, ErrorMessage = "", MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Path { get; set; }

        public double Likes { get; set; }

        [Required]
        public User Author { get; set; }
    }
}