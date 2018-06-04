using BubaTube.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts
{
    public interface IUploadVideoService
    {
        void SaveToDatabase(VideoDTO video);
    }
}
