using EnvRnk.DataAccess.DbModels;
using System;
using System.Threading.Tasks;

namespace EnvRnk.Services.Services
{
    public interface IRnkUserService
    {
        Task<RnkUser> Create(Guid aspUserId);
        Task<RnkUser> GetByAspId(Guid aspId, bool withAspUser = false);
    }
}