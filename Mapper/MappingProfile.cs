using AutoMapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Private, PrivateResponseDTO>();
    }
}