using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using hotel_reservation_api.Core.DTOs.Country;
using hotel_reservation_api.Core.Interfaces;
using hotel_reservation_api.Data;
using Microsoft.EntityFrameworkCore;

namespace hotel_reservation_api.Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly HotelReservationDBContext _context;
        private readonly IMapper _mapper;

        public CountryService(HotelReservationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var countries = await _context.Countries.ToListAsync();
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return null;

            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> CreateAsync(CreateCountryDto dto)
        {
            var country = _mapper.Map<Country>(dto);

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return _mapper.Map<CountryDto>(country);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCountryDto dto)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return false;

            _mapper.Map(dto, country); // এক্সিস্টিং এনটিটিতে ডাটা আপডেট করে নেওয়া

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return false;

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}