using System;
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
    public class HotelService : IHotelService
    {
        private readonly HotelReservationDBContext _context;
        private readonly IMapper _mapper;

        public HotelService(HotelReservationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            var hotels = await _context.Hotels
                .Include(h => h.Country)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto?> GetByIdAsync(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Country)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null) return null;

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<HotelDto> CreateAsync(CreateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            // কান্ট্রির নামসহ রিটার্ন করার জন্য রিলেশন রিলোড করা
            var createdHotel = await _context.Hotels
                .Include(h => h.Country)
                .FirstAsync(h => h.Id == hotel.Id);

            return _mapper.Map<HotelDto>(createdHotel);
        }

        public async Task<bool> UpdateAsync(int id, UpdateHotelDto dto)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _mapper.Map(dto, hotel);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}