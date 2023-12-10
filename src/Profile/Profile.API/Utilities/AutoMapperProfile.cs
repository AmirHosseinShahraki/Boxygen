using Profile.API.DTOs;
using Profile.Domain.Entities;

namespace Profile.API.Utilities;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SubmitUserProfileDto, UserProfile>();
        CreateMap<UpdateUserProfileDto, UserProfile>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}