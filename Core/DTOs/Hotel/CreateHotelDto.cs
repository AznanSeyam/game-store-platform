using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_reservation_api.Core.DTOs.Hotel
{
    public class CreateHotelDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}