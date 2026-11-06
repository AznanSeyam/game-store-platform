using AutoMapper;
using hotel_reservation_api.Core.DTOs.Country;
using hotel_reservation_api.Core.DTOs.Hotel;
using hotel_reservation_api.Data;

namespace hotel_reservation_api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<CreateCountryDto, Country>();
        CreateMap<UpdateCountryDto, Country>();

        CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : string.Empty));
        CreateMap<CreateHotelDto, Hotel>();
        CreateMap<UpdateHotelDto, Hotel>();
    }
}