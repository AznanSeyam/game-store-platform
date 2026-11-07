/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using hotel_reservation_api.Core.DTOs.Hotel;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Data;
using Microsoft.EntityFrameworkCore;

namespace hotel_reservation_api.Core.Services
{
    public class HotelService(HotelReservationDBContext context, IMapper mapper) : IHotelService
    {


        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            var hotels = await context.Hotels
                .Include(h => h.Country)
                .ToListAsync();

            return mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto?> GetByIdAsync(int id)
        {
            var hotel = await context.Hotels
                .Include(h => h.Country)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null) return null;

            return mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> CreateAsync(CreateHotelDto dto)
        {
            var hotel = mapper.Map<Hotel>(dto);

            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();

            var createdHotel = await context.Hotels
                .Include(h => h.Country)
                .FirstAsync(h => h.Id == hotel.Id);

            return mapper.Map<HotelDto>(createdHotel);
        }

        public async Task<bool> UpdateAsync(int id, UpdateHotelDto dto)
        {
            var hotel = await context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            mapper.Map(dto, hotel);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

*/
