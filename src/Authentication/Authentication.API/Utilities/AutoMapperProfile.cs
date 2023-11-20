using Authentication.API.DTOs;
using Authentication.Domain.Commands;
using AutoMapper;

namespace Authentication.API.Utilities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegistrationDto, RegisterUser>();
        CreateMap<LoginDto, LoginUser>();
    }
}