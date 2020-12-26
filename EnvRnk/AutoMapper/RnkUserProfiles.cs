using AutoMapper;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.Services.DTOs.RnkUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvRnk.AutoMapper
{
    public class RnkUserProfiles : Profile
    {
        public RnkUserProfiles()
        {
            CreateMap<RnkUser, RnkUserForArticleList>();
        }
    }
}
