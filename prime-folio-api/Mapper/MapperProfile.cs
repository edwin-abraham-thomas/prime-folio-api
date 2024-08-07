﻿using AutoMapper;
using Models.Requests;

namespace Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //User Maps

            CreateMap<Models.User, entities.User>()
                .ReverseMap();

            CreateMap<UserCreateRequest, entities.User>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<UserCreateRequest, Models.User>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<UserUpdateRequest, entities.User>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<UserUpdateRequest, Models.User>()
                .ReverseMap();

            CreateMap<UserCreateOrVerifyRequest, UserCreateRequest>()
                .ReverseMap();

            //Content Maps

            CreateMap<Models.Content, entities.Content>()
                .ReverseMap();
            CreateMap<Models.Dimension, entities.Dimension>()
                .ReverseMap();
            CreateMap<Models.Tile, entities.Tile>()
                .ReverseMap();
            CreateMap<Models.TileContent, entities.TileContent>()
                .ReverseMap();

            CreateMap<ContentCreateRequest, entities.Content>();
            CreateMap<ContentCreateRequest, Models.Content>();

            CreateMap<ContentUpdateRequest, entities.Content>();
            CreateMap<entities.Content, Models.Content>();
        }
    }
}
