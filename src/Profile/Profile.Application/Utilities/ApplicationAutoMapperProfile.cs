using Profile.Domain.Entities;
using Shared.Events;

namespace Profile.Application.Utilities;

public class ApplicationAutoMapperProfile : AutoMapper.Profile
{
    public ApplicationAutoMapperProfile()
    {
        CreateMap<UserProfile, UserProfile>()
            .ForMember(p => p.Id, opt => opt.PreCondition(src => src.Id != Guid.Empty))
            .ForMember(p => p.CreatedAt, opt => opt.Ignore())
            .ForMember(p => p.UpdatedAt, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserProfile, ProfileSubmitted>()
            .ForMember(e => e.CorrelationId, opt => opt.MapFrom(dist => dist.Id));
    }
}