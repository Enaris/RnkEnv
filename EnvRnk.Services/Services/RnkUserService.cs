using EnvRnk.DataAccess.DbModels;
using EnvRnk.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnvRnk.Services.Services
{
    public class RnkUserService : IRnkUserService
    {
        private readonly IRnkUserRepository rnkUserRepo;

        public RnkUserService(IRnkUserRepository rnkUserRepo)
        {
            this.rnkUserRepo = rnkUserRepo;
        }

        public async Task<RnkUser> GetByAspId(Guid aspId, bool withAspUser = false)
        {
            return
                withAspUser ?
                await rnkUserRepo
                    .GetAll()
                    .Include(u => u.AspUser)
                    .FirstOrDefaultAsync(u => u.AspUserId == aspId)
                :
                await rnkUserRepo
                    .GetAll()
                    .FirstOrDefaultAsync(u => u.AspUserId == aspId);
        }

        public async Task<RnkUser> Create(Guid aspUserId)
        {
            var userToAdd = new RnkUser { AspUserId = aspUserId };
            await rnkUserRepo.CreateAsync(userToAdd);
            await rnkUserRepo.SaveChangesAsync();
            return userToAdd;
        }
    }
}
