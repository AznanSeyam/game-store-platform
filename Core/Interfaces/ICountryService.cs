using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Country;

namespace hotel_reservation_api.Core.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetAllAsync();
    Task<CountryDto?> GetByIdAsync(int id);
    Task<CountryDto> CreateAsync(CreateCountryDto dto);
    Task<bool> UpdateAsync(int id, UpdateCountryDto dto);
    Task<bool> DeleteAsync(int id);
}