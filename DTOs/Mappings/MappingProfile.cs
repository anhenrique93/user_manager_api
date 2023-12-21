using AutoMapper;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, AddUserDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
        CreateMap<Gender, GenderDTO>().ReverseMap();
    }
}
