using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Data.DTO
{
    public class VideoPreviewDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public UserPreviewDTO Author { get; set; }
    }
}
