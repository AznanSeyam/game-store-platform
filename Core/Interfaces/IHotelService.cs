using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Hotel;

namespace hotel_reservation_api.Core.Interfaces;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllAsync();
    Task<HotelDto?> GetByIdAsync(int id);
    Task<HotelDto> CreateAsync(CreateHotelDto dto);
    Task<bool> UpdateAsync(int id, UpdateHotelDto dto);
    Task<bool> DeleteAsync(int id);
}