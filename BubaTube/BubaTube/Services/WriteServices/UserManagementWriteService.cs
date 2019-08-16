﻿using BubaTube.Data;
using BubaTube.Data.Models;
using BubaTube.Services.Contracts.Write;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.WriteServices
{
    public class UserManagementWriteService : IUserManagementWriteService
    {
        private BubaTubeDbContext context;

        public UserManagementWriteService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveLoginDate(string userID)
        {
            this.context.Users
                .FirstOrDefault(x => x.Id == userID)
                .LastLogin = DateTime.Now;
            var result = await context.SaveChangesAsync();

            return result;
        }
    }
}
