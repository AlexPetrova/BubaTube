using System.Collections.Generic;
using System.Linq;
using BubaTube.Areas.Admin.Servises.Contracts;

namespace BubaTube.Areas.Admin.Servises
{
    public class AdminService : IAdminService
    {
        private readonly BubaTubeDbContext context;

        public AdminService(BubaTubeDbContext context)
        {
            this.context = context;
        }
        
        
    }
}
