﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.Models
{
    public class Tag : BaseModel
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Content { get; set; }
    }
}
