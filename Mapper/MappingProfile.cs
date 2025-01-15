using AutoMapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Private, PrivateResponseDTO>();

        CreateMap<PrivateUpdateRequestDTO, Private>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                src.TagIds.Select(tagId => new PrivateTag { TagId = tagId })))
            .ForMember(dest => dest.Subways, opt => opt.MapFrom(src =>
                src.SubwayIds.Select(subwayId => new PrivateSubway { SubwayId = subwayId })));
    }
}