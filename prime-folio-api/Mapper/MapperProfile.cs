using AutoMapper;
using Models.Requests;

namespace Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Models.User, entities.User>()
                .ReverseMap();

            CreateMap<UserCreateRequest, entities.User>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<UserCreateRequest, Models.User>()
                .ReverseMap();

            CreateMap<UserUpdateRequest, entities.User>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<UserUpdateRequest, Models.User>()
                .ReverseMap();
        }
    }
}
