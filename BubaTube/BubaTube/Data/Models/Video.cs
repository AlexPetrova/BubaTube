﻿using BubaTube.Data.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="")]
        public string Name { get; set; } //or path?

        [Required]
        public Category Category { get; set; }

        public double Likes { get; set; }

        [Required]
        public string UserId { get; set; }

        public User Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserVideo> UserVideo { get; set; }
    }
}