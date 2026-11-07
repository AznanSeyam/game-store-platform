/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using hotel_reservation_api.Core.DTOs.Country;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Data;
using Microsoft.EntityFrameworkCore;

namespace hotel_reservation_api.Core.Services;

public class CountryService(HotelReservationDBContext context, IMapper mapper) : ICountryService
{


    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await context.Countries.ToListAsync();
        return mapper.Map<IEnumerable<CountryDto>>(countries);
    }

    public async Task<CountryDto?> GetByIdAsync(int id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null) return null;

        return mapper.Map<CountryDto>(country);
    }

    public async Task<CountryDto> CreateAsync(CreateCountryDto dto)
    {
        var country = mapper.Map<Country>(dto);

        context.Countries.Add(country);
        await context.SaveChangesAsync();

        return mapper.Map<CountryDto>(country);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCountryDto dto)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null) return false;

        mapper.Map(dto, country);

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null) return false;

        context.Countries.Remove(country);
        await context.SaveChangesAsync();
        return true;
    }
}

*/
