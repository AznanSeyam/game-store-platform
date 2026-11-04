using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_reservation_api.Core.DTOs.Country
{
    public class CreateCountryDto
    {
        public required string Name { get; set; }
        public required string ShortName { get; set; }
    }
}