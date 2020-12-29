using AutoMapper;
using EnvRnk.DataAccess.DbModels;
using EnvRnk.Services.DTOs.Point;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvRnk.AutoMapper
{
    public class UserArticlePointProfiles : Profile
    {
        public UserArticlePointProfiles()
        {
            CreateMap<UserArticlePoint, PointForArticleDetails>()
                .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User.AspUser.Email));
        }
    }
}
