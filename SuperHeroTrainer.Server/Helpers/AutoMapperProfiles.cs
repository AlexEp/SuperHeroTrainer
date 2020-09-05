using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SuperHeroTrainer.Core.Identity;
using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Entities;

namespace SuperHeroTrainer.Models.Mappers
{

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegisterModal, UserDTO>()
                .ForMember(dest => dest.UserName, opt => {
                    opt.MapFrom(src => src.Username);
                })
                .ForMember(dest => dest.Email, opt => {
                         opt.MapFrom(src => src.Email);
                });

            CreateMap<AppIdentityUser, UserInfoDTO>()
                .ForMember(dest => dest.UserName, opt =>
                {
                    opt.MapFrom(src => src.UserName);
                });

            CreateMap<UserDTO, AppIdentityUser>();
            CreateMap<AppIdentityUser,UserDTO>();

            CreateMap<Hero, HeroDTO>()
                .ForMember(dest => dest.SuitColor, opt =>
            {
                opt.MapFrom<ColorResolver>();
            }); ;
        }

        public class ColorResolver : IValueResolver<Hero, HeroDTO, string>
        {
            public string Resolve(Hero source, HeroDTO destination, string member, ResolutionContext context)
            {
                var color = Color.FromArgb(source.SuitColor);
                var hex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                return hex;
            }
        }
    }
}
