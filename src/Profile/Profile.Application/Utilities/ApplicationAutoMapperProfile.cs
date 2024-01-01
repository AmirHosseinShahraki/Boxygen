using Profile.Domain.Entities;
using Shared.Events;

namespace Profile.Application.Utilities;

public class ApplicationAutoMapperProfile : AutoMapper.Profile
{
    public ApplicationAutoMapperProfile()
    {
        CreateMap<UserProfile, UserProfile>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.Email, opt => opt.Condition(src => src.Email != null))
            .ForMember(p => p.FullName, opt => opt.Condition(src => src.FullName != null))
            .ForMember(p => p.Phone, opt => opt.Condition(src => src.Phone != null))
            .ForMember(p => p.ImageUri, opt => opt.Condition(src => src.ImageUri != null))
            .ForMember(p => p.DateOfBirth,
                opt => opt.Condition(src => src.DateOfBirth != null && src.DateOfBirth != DateTime.MinValue))
            .ForMember(p => p.UpdatedAt, opt => opt.Ignore())
            .ForMember(p => p.CreatedAt, opt => opt.Ignore());

        CreateMap<UserProfile, ProfileSubmitted>()
            .ForMember(e => e.CorrelationId, opt => opt.MapFrom(dist => dist.Id));
    }
}