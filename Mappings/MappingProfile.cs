using AutoMapper;
using game_store_platform.Core.DTOs.Game;
using game_store_platform.Core.DTOs.Order;
using game_store_platform.Data.Entities;
using game_store_platform.Core.DTOs.Genre;

namespace game_store_platform.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();



            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.GenreName,
                           opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : string.Empty));

            CreateMap<CreateGameDto, Game>();
            CreateMap<UpdateGameDto, Game>();



            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.GameTitle,
                           opt => opt.MapFrom(src => src.Game != null ? src.Game.Title : string.Empty))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}