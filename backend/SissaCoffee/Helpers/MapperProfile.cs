using AutoMapper;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.User;

namespace SissaCoffee.Helpers
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, LoginUserDTO>();
            CreateMap<ApplicationUser, RegisterUserDTO>();
        }

    }
}
